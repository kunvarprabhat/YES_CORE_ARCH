using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YES.Comman;
using YES.Comman.Interfaces;
using YES.Domain.EntityClasses;
using YES.Dtos;
using YES.Dtos.Student;
using YES.Services.IServices;
using YES.Utility.Constants;

namespace YES.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IRegistrationService _registration;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly FileUploadSettings _settings;
        private readonly IExamService _examService;
        public StudentController(IRegistrationService registration, ICourseService courseService, IStudentService studentService, FileUploadSettings settings,
            IExamService examService,
            ICurrentUserService currentUserService) : base(currentUserService)
        {
            _registration = registration;
            _courseService = courseService;
            _studentService = studentService;
            _settings = settings;
            _examService = examService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var res = await _studentService.GetAll(BranchId, BranchType);
            return Json(res.Data);
        }
        public async Task<IActionResult> Application(int id, string RId)
        {
            var studentDto = new StudentDto();
            var res = await _courseService.GetAllAsync();
            ViewBag.CourseList = res.Data;
            var res1 = await _registration.GetAll(BranchId, BranchType);
            var newRegistratin = res1.Data.Select(x => new
            {
                x.Id,
                x.RegistrationNo
            }).ToList();
            if (!string.IsNullOrEmpty(RId))
            {
                var registrationData = await _registration.GetById(RId);
                studentDto.RegistrationDto = registrationData.Data;
            }
            if (id != 0)
            {
                var result = await _studentService.GetById(id);
                studentDto = result.Data;
                newRegistratin.Add(
                    new
                    {
                        studentDto.RegistrationDto.Id,
                        studentDto.RegistrationDto.RegistrationNo
                    });
            }

            ViewBag.RegList = newRegistratin;
            return View(studentDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentDto studentDto)
        {
            studentDto.CreatedBy = UserId;
            studentDto.BranchId = BranchId;
            studentDto.BranchType = BranchType;
            if (ModelState.IsValid)
            {
                return Json(studentDto);
            }
            if (studentDto.Id != 0 && !string.IsNullOrEmpty(studentDto.StudentId))
            {
                var res = await _studentService.Edit(studentDto);
                return Json(res);
            }
            else
            {
                var res = await _studentService.Create(studentDto);
                return Json(res);

            }

        }
        public async Task<IActionResult> GetRegistrationById(int id)
        {
            var res = await _registration.GetById(id);
            return Json(res.Data);
        }
        [AllowAnonymous]
        [Route("result-verification")]
        public async Task<IActionResult> ResultVerification(string StudentId)
        {
            StudentDto dto = new StudentDto();

            if (!string.IsNullOrEmpty(StudentId))
            {
                var res = await _studentService.GetResultByRIdOrSId(StudentId);
                if (res.Success && res.Data != null)
                    return View(res.Data);
            }
            return View(dto);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("certificate-verification")]
        public async Task<IActionResult> CertificateVerification(string StudentId)
        {
            StudentDto dto = new StudentDto();

            if (!string.IsNullOrEmpty(StudentId))
            {
                var res = await _studentService.GetResultByRIdOrSId(StudentId);
                if (res.Success && res.Data != null)
                    return View(res.Data);
            }
            return View(dto);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Verification(string RegId, string SId)
        {
            if (!string.IsNullOrEmpty(RegId))
            {
                var res = await _studentService.GetResultByRIdOrSId(RegId);
                if (res.Success && res.Data != null)
                    return PartialView("~/Views/Marksheet/_MarksheetPartialView.cshtml", res.Data);
            }
            else if (!string.IsNullOrEmpty(SId))
            {
                var res = await _studentService.GetResultByRIdOrSId(SId);
                if (res.Success && res.Data != null)
                    return PartialView("~/Views/Certificate/_CertificatePartialView.cshtml", res.Data);
            }
            return Json(new APIResponse
            {
                Message = "Please enter Your Application Number/Student Id",
                Success = false,
                Status = System.Net.HttpStatusCode.OK,
            });
        }
        public async Task<IActionResult> StudentProfile(int id)
        {
            var res = await _studentService.GetProfileById(id);
            var examModules = await _examService.GetAllByCourseIdAsync(res.Data.CourseId);
            ViewBag.examModules = examModules.Data;
            return View(res.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(FileSystemViewModel vm)
        {
            try
            {
                StudentProfileImage profileImage = new StudentProfileImage();
                vm.UploadSettings = _settings;
                vm.Height = Constants.ProfileHeight;
                vm.Width = Constants.ProfileWidth;
                vm.PrefixName = $"Profile_";
                if (ModelState.IsValid)
                {
                    if (await vm.Save())
                    {
                        profileImage.StudentId = vm.StudentId;
                        profileImage.ProfileImageName = vm.FileInfo.FileName;
                        profileImage.ProfileFolderPath = "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName;
                    }

                    var res = await _studentService.UploadProfileImage(profileImage);
                    return Json(res);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { Message = "Test" });

        }


    }
}