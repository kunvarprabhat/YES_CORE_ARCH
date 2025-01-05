using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos.API;
using YES.Dtos.Student;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;
using YES.Dtos.Course;
using YES.Utility.Enums;
using YES.Dtos.Result;

namespace YES.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        public async Task<APIResponse<StudentDto>> Create(StudentDto studentDto)
        {
            APIResponse<StudentDto> response = new APIResponse<StudentDto>();

            try
            {
                Student student = new Student
                {
                    EducationalDetails = studentDto.EducationalDetailDtos.Where(x => x.BoardName != null).Select(x => new EducationalDetail
                    {
                        BoardName = x.BoardName,
                        ExamName = x.ExamName,
                        ObtainedMarks = x.ObtainedMarks,
                        Percentage = x.Percentage,
                        YearOfPassing = x.YearOfPassing,
                        IsSelected = x.IsSelected
                    }).ToList(),
                    BranchType = studentDto.BranchType,
                    BranchId = studentDto.BranchId,
                    CourseId = studentDto.CourseId,
                    CreatedBy = studentDto.CreatedBy,
                    RegistrationId = studentDto.RegistrationDto.Id,
                    Category = studentDto.Category,

                };
                var result = await _studentRepository.Create(student);

                if (result != null)
                {
                    response.Success = true;
                    response.Message = CommonResource.AddSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.AddFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }

        public Task<APIResponse> Delete(int RId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Edit(StudentDto studentDto)
        {
            var response = new APIResponse();

            try
            {
                Student student = new Student
                {
                    EducationalDetails = studentDto?.EducationalDetailDtos!.Where(x => x.BoardName != null).Select(x => new EducationalDetail
                    {
                        BoardName = x.BoardName,
                        ExamName = x.ExamName,
                        ObtainedMarks = x.ObtainedMarks,
                        Percentage = x.Percentage,
                        YearOfPassing = x.YearOfPassing,
                        Id = x.Id
                    }).ToList(),
                    BranchType = studentDto.BranchType,
                    BranchId = studentDto.BranchId,
                    CourseId = studentDto.CourseId,
                    UpdatedBy = studentDto.CreatedBy,
                    RegistrationId = studentDto.RegistrationDto.Id,
                    Category = studentDto.Category,
                    Id = studentDto.Id

                };
                var result = await _studentRepository.Edit(student);

                if (result != null)
                {
                    response.Success = true;
                    response.Message = CommonResource.UpdateSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.UpdateFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.UpdateFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }

        public async Task<APIResponse<IEnumerable<StudentDto>>> GetAll(int BranchId, int BranchType)
        {
            APIResponse<IEnumerable<StudentDto>> response = new APIResponse<IEnumerable<StudentDto>>();

            try
            {
                var result = await _studentRepository.GetAll(BranchId, BranchType);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = result.Select(x => new StudentDto
                    {
                        Category = x.Category,
                        Id = x.Id,
                        StudentId = x.StudentId,
                        Nationality = x.Nationality,
                        ProfilePath = x.ProfilePath,
                        SignatureImage = x.SignatureImage,
                        SignaturePath = x.SignaturePath,
                        Status = (int)StudentStatus.Application,
                        ProfileName = x.ProfileName,
                        CourseId = x.CourseId,
                        RegistrationDto = _mapper.Map<RegistrationDto>(x.Registration),
                    }).ToList();
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;

                throw;
            }


            return response;
        }

        public async Task<APIResponse<StudentDto>> GetById(int RId)
        {
            APIResponse<StudentDto> response = new APIResponse<StudentDto>();

            try
            {
                var result = await _studentRepository.GetById(RId);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<StudentDto>(result);
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;

                throw;
            }


            return response;
        }

        public async Task<APIResponse<StudentDto>> GetProfileById(int SId)
        {
            APIResponse<StudentDto> response = new APIResponse<StudentDto>();

            try
            {
                var result = await _studentRepository.GetProfileById(SId);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<StudentDto>(result);
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;

                throw;
            }


            return response;
        }

        public async Task<APIResponse<StudentDto>> GetResultByRIdOrSId(string SId)
        {
            APIResponse<StudentDto> response = new APIResponse<StudentDto>();

            try
            {
                var result = await _studentRepository.GetResultByRIdOrSId(SId);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<StudentDto>(result);
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;

                throw;
            }


            return response;
        }

        public async Task<APIResponse> UploadProfileImage(StudentProfileImage profileImage)
        {
            APIResponse response = new APIResponse();
            try
            {
                Student student = new Student
                {
                    Id = profileImage.StudentId,
                    ProfileName = profileImage.ProfileImageName,
                    ProfilePath = profileImage.ProfileFolderPath,
                    SignatureImage = profileImage.SignatureImageName,
                    SignaturePath = profileImage.SignatureFolderPath,
                };
                var res = await _studentRepository.UploadProfileImage(student);
                if (res > 0)
                {
                    response.Success = true;
                    response.Message = CommonResource.UpdateSuccess;
                    response.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;

                throw;
            }


            return response;
        }
    }
}
