using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.Student;
using YES.Services.IServices;
namespace YES.Controllers.Admin
{
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registration;
        public RegistrationController(IRegistrationService registration, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _registration = registration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var res = await _registration.GetAll(BranchId, BranchType);
            return Json(res.Data);
        }
        [AllowAnonymous]
        [HttpGet("regitration-form")]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Registration(int id)
        {
            RegistrationDto regitrationDto = new RegistrationDto();
            if (id != 0)
            {
                regitrationDto = _registration.GetById(id).Result.Data;
                string[] dob = regitrationDto.Dob.Split('/');
                regitrationDto.Day = dob[0];
                regitrationDto.Month = dob[1];
                regitrationDto.Years = dob[2];
            }
            return View(regitrationDto);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDto regitrationDto)
        {
            regitrationDto.Id = regitrationDto.Id == 0 ? 0 : regitrationDto.Id;
            if (ModelState.IsValid)
            {
                if (regitrationDto.Id != 0)
                {
                    regitrationDto.UpdatedBy = UserId;
                    regitrationDto.BranchId = BranchId;
                    regitrationDto.BranchType = BranchType;
                    var res = await _registration.Edit(regitrationDto);
                    return Json(res);
                }
                else
                {
                    regitrationDto.CreatedBy = UserId;
                    regitrationDto.BranchId = BranchId;
                    regitrationDto.BranchType = BranchType;
                    var res = await _registration.Create(regitrationDto);
                    return Json(res);
                }

            }

            return Json(regitrationDto);
        }
    }
}
