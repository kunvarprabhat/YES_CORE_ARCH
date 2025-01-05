using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;
using YES.Domain.Auth;
using YES.Dtos;
using YES.Dtos.Account;
using YES.Dtos.API;
using YES.Dtos.Branch;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Constants;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IAccountRepository accountRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<APIResponse<BranchDto>> Login(LoginDto loginDto)
        {
            var response = new APIResponse<BranchDto>();
            try
            {

                var Model = new ApplicationUser
                {
                    UserName = loginDto.UserName,
                    Password = loginDto.Password,
                };
                var result = await _accountRepository.Login(Model);
                if (result != null && result.Data != null)
                {
                    response.Data = _mapper.Map<BranchDto>(result.Data);
                    response.Message = result.Message;
                    response.Status = result.Status;
                    response.Success = result.Success;

                }
                else
                {
                    response.Status = result.Status;
                    response.Error = result.Error;
                    response.Message = result.Message;
                    response.Success = result.Success;
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
