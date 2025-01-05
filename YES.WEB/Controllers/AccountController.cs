using Microsoft.AspNetCore.Mvc;
using System.Net;
using YES.Dtos.Account;
using YES.Services.IServices;
using YES.Dtos;
using Microsoft.AspNetCore.Identity;
using YES.Domain.Auth;
using YES.Services.ComunicationServices;
using YES.Dtos.Mail;

namespace YES.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICommunication _communication;

        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ICommunication communication)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _communication = communication;
        }
        [Route("/auth")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _accountService.Login(loginDto);
            if (res != null && res.Data != null && res.Success && res.Status == HttpStatusCode.OK)
            {
                //SetUerDataInClaim(res.Data);
            }
            return Json(res);
        }
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData.Clear();
                foreach (var cokies in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cokies);

                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!Equals(model.NewPassword.Trim(), model.ConfirmPassword))
            {
                return Json(new APIResponse
                {
                    Message = "New Password and confirm password mis match. please check it!!",
                    Success = false,
                    Status = HttpStatusCode.Unauthorized
                });
            }
            if (User.Identity == null)
            {
                return RedirectToAction("Index", "Account");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return Json(new APIResponse
                {
                    Message = "",
                    Status = HttpStatusCode.OK,
                    Success = true
                });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = model.Email }, protocol: Request.Scheme);

            await _communication.SendEmailAsync(new SendMailDto
            {
                ToAddress = model.Email,
                Subject = "Reset Password",
                Body = $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>"
            });
            return Json("");
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset token.");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }
    }
}
