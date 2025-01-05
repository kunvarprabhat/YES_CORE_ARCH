using AutoMapper;
using System.Net;
using System.Security.Cryptography;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Student;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;
        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _registrationRepository = registrationRepository;
        }
        public async Task<APIResponse<RegistrationDto>> Create(RegistrationDto regitrationDto)
        {
            APIResponse<RegistrationDto> response = new APIResponse<RegistrationDto>();

            try
            {
                var Model = _mapper.Map<Registration>(regitrationDto);
                var result = await _registrationRepository.Create(Model);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<RegistrationDto>(result.Data);
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

        public Task<APIResponse> Delete(int RId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Edit(RegistrationDto regitrationDto)
        {
            var Model = _mapper.Map<Registration>(regitrationDto);
            return await _registrationRepository.Edit(Model);
        }

        public async Task<APIResponse<IEnumerable<RegistrationDto>>> GetAll(int BranchId, int BranchType)
        {
            APIResponse<IEnumerable<RegistrationDto>> response = new APIResponse<IEnumerable<RegistrationDto>>();

            try
            {
                var result = await _registrationRepository.GetAll(BranchId, BranchType);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<RegistrationDto>>(result);
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


        public async Task<APIResponse<RegistrationDto>> GetById(int RId)
        {
            APIResponse<RegistrationDto> response = new APIResponse<RegistrationDto>();

            try
            {
                var result = await _registrationRepository.GetById(RId);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<RegistrationDto>(result);
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

        public async Task<APIResponse<RegistrationDto>> GetById(string RegistrationNo)
        {
            APIResponse<RegistrationDto> response = new APIResponse<RegistrationDto>();
            var result = await _registrationRepository.GetById(RegistrationNo);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<RegistrationDto>(result);
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}

