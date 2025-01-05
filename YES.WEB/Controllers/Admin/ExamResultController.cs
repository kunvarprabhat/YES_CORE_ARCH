using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.Result;
using YES.Services.IServices;
using YES.Services.Services;
using YES.Utility.Common;

namespace YES.Controllers.Admin
{
    public class ExamResultController : BaseController
    {
        private readonly IExamResultService _examResult;
        private readonly IExamService _examService;
        public ExamResultController(IExamResultService examResult,
            IExamService examService,
            ICurrentUserService currentUserService) : base(currentUserService)
        {
            _examResult = examResult;
            _examService = examService;
        }
        public async Task<IActionResult> GetAll()
        {
            var res = await _examResult.GetAllAsync();
            return Json(res);
        }
        public async Task<IActionResult> GetById(int id, int CourseId)
        {
            var res = await _examResult.GetByIdAsync(id);
            var examModules = await _examService.GetAllByCourseIdAsync(CourseId);
            ViewBag.examModules = examModules.Data;
            return PartialView("~/views/Student/_ResultEntry.cshtml", res.Data);
        }
        public async Task<IActionResult> GetAllByStudentId(int Id)
        {
            var res = await _examResult.GetAllByStudentId(Id);
            if (res.Data != null)
            {
                return PartialView("~/views/Student/_ExamResult.cshtml", res.Data);
            }

            return Json(res);

        }
        [HttpPost]
        public async Task<IActionResult> Create(ExamResultDto examResultDto)
        {
            examResultDto.Percentage = examResultDto.ObtainMark / examResultDto.TotalMarks * 100;
            examResultDto.Grad = examResultDto.Percentage.Grad();
            if (ModelState.IsValid)
            {
                if (examResultDto.Id != null)
                {
                    var res = await _examResult.EditAsync(examResultDto);
                    return Json(res);
                }
                else
                {
                    var res = await _examResult.CreateAsync(examResultDto);
                    return Json(res);

                }
            }
            else
            {
                return Json(new { Message = "Some field is reqired", success = "false" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var examResult = new ExamResultDto();
            examResult.Id = id;
            var res = await _examResult.DeleteAsync(examResult);
            return Json(res);
        }

    }
}
