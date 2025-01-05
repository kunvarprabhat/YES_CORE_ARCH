using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Branch;
using YES.Dtos.Mail;
using YES.Infrastructure.IRepositories;
using YES.Services.ComunicationServices;
using YES.Services.IServices;
using YES.Utility.Constants;
using YES.Utility.Resources;
using YES.Utility.Common;

namespace YES.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly ICommunication _communication;
        public BranchService(IBranchRepository branchRepository, IMapper mapper, ICommunication communication)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _communication = communication;
        }
        public async Task<APIResponse<BranchDto>> CreateAsync(BranchDto entity)
        {
            APIResponse<BranchDto> response = new APIResponse<BranchDto>();

            try
            {
                var Model = _mapper.Map<Branch>(entity);
                var result = await _branchRepository.Create(Model);

                if (result.Data != null && result.Data.User != null)
                {
                    var res = await _communication.SendEmailAsync(new SendMailDto
                    {
                        ToAddress = entity.EmailId,
                        Subject = "Your Account Details",
                        Body = string.Format(Constants.BranchCreatedMessage, (entity.Name + " " + entity.LastName), entity.EmailId, result.Data.User.Password, "http://YES.in/auth")
                    });

                    if (!res.Success)
                    {

                    }

                    response.Data = _mapper.Map<BranchDto>(result.Data);
                    response.Message = result.Message;
                    response.Success = result.Success;
                    response.Status = result.Status;
                }
                else
                {
                    response.Success = result.Success;
                    response.Message = result.Message;
                    response.Status = result.Status;
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

        public async Task<APIResponse> DeleteAsync(BranchDto entity)
        {
            APIResponse response = new APIResponse();
            var Model = _mapper.Map<Branch>(entity);
            var result = await _branchRepository.DeleteRecord(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.DeleteSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.DeleteFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BranchDto>> EditAsync(BranchDto entity)
        {
            APIResponse<BranchDto> response = new APIResponse<BranchDto>();

            try
            {
                var Model = _mapper.Map<Branch>(entity);
                var result = await _branchRepository.Edit(Model);
                if (result != 0)
                {
                    response.Success = true;
                    response.Message = CommonResource.UpdateSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Data = entity;
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

        public async Task<APIResponse<IEnumerable<BranchDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<BranchDto>> response = new APIResponse<IEnumerable<BranchDto>>();

            try
            {
                var result = await _branchRepository.GetAll();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<BranchDto>>(result);
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

        public async Task<APIResponse<BranchDto>> GetBranchDetailsAsync(int id)
        {
            APIResponse<BranchDto> response = new APIResponse<BranchDto>();

            try
            {
                var result = await _branchRepository.GetBranchDetails(id);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<BranchDto>(result);
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

        public async Task<APIResponse<BranchDto>> GetByIdAsync(int Id)
        {
            APIResponse<BranchDto> response = new APIResponse<BranchDto>();

            try
            {
                var result = await _branchRepository.GetById(Id);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<BranchDto>(result);
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

        public async Task<APIResponse> UpdateStatustAsync(int id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var result = await _branchRepository.UpdateStatus(id);

                if (result != 0)
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
    }
}
