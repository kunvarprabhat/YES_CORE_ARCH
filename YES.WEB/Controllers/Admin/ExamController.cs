using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces; 
using YES.Dtos; 
using YES.Dtos.ExamResult;
using YES.Services.IServices;

namespace YES.Controllers.Admin
{
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;
        private readonly ICourseService _courseService;
        public ExamController(IExamService examService, ICourseService courseService, ICurrentUserService userService) : base(userService)
        {
            _examService = examService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            ExamVm examVm = new ExamVm();
            var res = await _courseService.GetAllAsync();
            if (res.Success && res.Data != null)
            {
                examVm.Courses = res.Data;
            }
            return View(examVm);
        }
        public async Task<IActionResult> GetAll() =>
            Json(await _examService.GetAllAsync());
        public async Task<IActionResult> GetById(int id)
        {
            ExamVm examVm = new ExamVm();
            examVm.examDtos = new List<ExamDto>(); // Initialize the list if it's not already initialized

            var res = await _examService.GetByIdAsync(id);
            if (res.Data != null && res.Success)
                examVm.examDtos.Add(new ExamDto
                {
                    CourseId = res.Data.CourseId,
                    ExamName = res.Data.ExamName,
                    Modules = res.Data.Modules,
                    Id = res.Data.Id
                });

            var result = await _courseService.GetAllAsync();
            if (result.Success && result.Data != null)
            {
                examVm.Courses = result.Data;
            }
            return PartialView("_ExamPartialView", examVm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExamVm examVm)
        {
            APIResponse aPIResponse = new APIResponse();
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "opps" });
            }
            if (examVm.examDtos != null && examVm.examDtos.Where(x => x.Id == null).Count() >= 1)
            {
                var examDtos = examVm.examDtos.Where(x => x.Id == null).Select(x => new ExamDto
                {
                    ExamName = x.ExamName,
                    Modules = x.Modules,
                    CourseId = x.CourseId,
                    CreatedBy = UserId,
                }).ToList();
                var result = await _examService.CreateAsync(examDtos);
                if (result.Success)
                {
                    aPIResponse.Success = result.Success;
                    aPIResponse.Message = result.Message;
                }
            }
            if (examVm.examDtos != null && examVm.examDtos.Where(x => x.Id != null).Count() == 1)
            {
                var examEdit = examVm.examDtos.Select(x => new ExamDto
                {
                    Id = x.Id,
                    ExamName = x.ExamName,
                    Modules = x.Modules,
                    CourseId = x.CourseId,
                    UpdatedBy = UserId
                }).First(x => x.CourseId != 0);
                var res = await _examService.EditAsync(examEdit);
            }
            return Json(aPIResponse);
        }

        public async Task<IActionResult> Delete(int id) =>
            Json(await _examService.DeleteAsync(new ExamDto { Id = id }));
    }
}
