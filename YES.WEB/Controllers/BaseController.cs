using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;

namespace YES.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        public BaseController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public string UserId
        {
            get
            {
                return _currentUserService.UserId;
            }
        }

        public string UserName
        {
            get
            {
                return _currentUserService.UserName;
            }
        }
        public string RoleId
        {
            get
            {
                return _currentUserService.RoleId;
            }
        }
        public string RoleName
        {
            get
            {
                return _currentUserService.RoleName;
            }
        }
        public int BranchId
        {
            get
            {
                return _currentUserService.BranchId;
            }
        }
        public int BranchType
        {
            get
            {
                return _currentUserService.BranchType;
            }
        }
    }
}
