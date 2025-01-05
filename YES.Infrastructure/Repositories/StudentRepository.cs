
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Common;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;

        public StudentRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext appContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = appContext;
        }
        public async Task<APIResponse<Student>> Create(Student student)
        {
            APIResponse<Student> apiResponse = new APIResponse<Student>();
            try
            {
                var data = _dbContext.Students.OrderByDescending(x => x.Id).FirstOrDefault();
                if (data != null)
                {
                    string digit = data.Id.ToString().GenrateRegNumber(data.Id + 1);
                    student.StudentId = "SID" + DateTime.Now.Year / 100 + "CC" + digit;
                }
                else
                {
                    student.StudentId = "SID" + DateTime.Now.Year / 100 + "CC00001";
                }

                var registration = await _dbContext.Registrations.Where(x => x.Id == student.RegistrationId).FirstAsync();
                registration.IsCompleted = true;
                _dbContext.Update(registration);
                Add(student);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.AddFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                throw;
            }
        }

        public Task<APIResponse> Delete(int RId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Edit(Student Student)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                var student = await _dbContext.Students.Include(x => x.EducationalDetails).Where(x => x.Id == Student.Id).FirstAsync();
                student.Category = Student.Category;
                student.UpdatedBy = Student.UpdatedBy;
                student.CourseId = Student.CourseId;

                student.EducationalDetails = Student.EducationalDetails;
                Update(student);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                if (result > 0)
                {
                    apiResponse.Success = true;
                    apiResponse.Message = CommonResource.UpdateSuccess;
                    apiResponse.Status = HttpStatusCode.OK;
                }
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.UpdateFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        public async Task<IEnumerable<Student>> GetAll(int BranchId, int BranchType)
        {
            return BranchType == 0 ? await _dbContext.Students.Include(x => x.Registration).Where(x => x.IsDeleted == false).ToListAsync() :
                await _dbContext.Students.Include(x => x.Registration).Where(x => x.IsDeleted == false && x.BranchId == BranchId).ToListAsync();
        }

        public async Task<Student> GetById(int SId)
        {
            return await _dbContext.Students.Include(x => x.Registration).Include(x => x.EducationalDetails).Where(x => !x.IsDeleted && x.Id == SId).FirstAsync();
        }
        public async Task<Student> GetProfileById(int SId)
        {
            return await _dbContext.Students.Include(x => x.Registration).Include(x => x.EducationalDetails).Include(x => x.Branch)
                .Include(x => x.Course).Include(x => x.ResultCertificate).Include(x => x.ExamResults.Where(x => !x.IsDeleted)).ThenInclude(x => x.Exams).Where(x => !x.IsDeleted && x.Id == SId).FirstAsync();
        }
        public async Task<Student> GetResultByRIdOrSId(string SId)
        {
            return await _dbContext.Students.Include(x => x.Registration).Include(x => x.EducationalDetails).Include(x => x.Branch)
                .Include(x => x.Course).Include(x => x.ResultCertificate).Include(x => x.ExamResults.Where(x => !x.IsDeleted)).ThenInclude(x => x.Exams).Where(x => !x.IsDeleted && (x.StudentId == SId || x.Registration.RegistrationNo==SId)).FirstAsync();
        }
        Expression<Func<Student, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<Student, bool>> FilterById(int RId)
        {
            return x => x.Id == RId;
        }

        public async Task<int> UploadProfileImage(Student student)
        {
            var data = await List(FilterById(student.Id)).FirstAsync();
            if (File.Exists(data.ProfilePath))
            {
                // If file found, delete it
                File.Delete(data.ProfilePath);
            }
            data.ProfileName = student.ProfileName;
            data.ProfilePath = student.ProfilePath;
            data.SignaturePath = student.SignaturePath;
            data.SignatureImage = student.SignatureImage;
            _dbContext.Students.Update(data);
            var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
