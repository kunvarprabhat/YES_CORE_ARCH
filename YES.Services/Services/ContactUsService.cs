using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Home;
using YES.Infrastructure.IRepositories;
using YES.Utility.Resources;
using Yesscc.Services.IServices;

namespace YES.Services.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IMapper _mapper;
        public ContactUsService(IContactUsRepository contactUsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _contactUsRepository = contactUsRepository;
        }
        public async Task<APIResponse<ContactUsDto>> CreateAsync(ContactUsDto contactUsDto)
        {
            APIResponse<ContactUsDto> response = new APIResponse<ContactUsDto>();

            try
            {
                contactUsDto.CreateDate = DateTime.Now;
                var Model = _mapper.Map<ContactUs>(contactUsDto);
                var courses = await _contactUsRepository.Create(Model);

                if (courses != null)
                {
                    response.Success = true;
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

        public async Task<APIResponse<IEnumerable<ContactUsDto>>> GetaAllAsync()
        {
            APIResponse<IEnumerable<ContactUsDto>> response = new APIResponse<IEnumerable<ContactUsDto>>();

            try
            {
                var result = await _contactUsRepository.GetAllCourse();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<ContactUsDto>>(result);
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
    }
}
