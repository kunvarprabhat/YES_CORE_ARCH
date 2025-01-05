using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Course;
using YES.Dtos.News;
using YES.Services.IServices;

namespace YES.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            CourseVm courseVm = new CourseVm();
            return View(courseVm);
        }
        [AllowAnonymous]
        public async Task<IActionResult> getAll()
        {
            var res = await _courseService.GetAllAsync();
            return Json(res);
        }

        [AllowAnonymous]
        [HttpGet("course-details/{id}")]
        public async Task<IActionResult> CourseDetails(int id)
        { 
            var res = await _courseService.GetCourseDetailsAsync(id); 
            return View(res.Data);
        }
        public async Task<IActionResult> getById(int id)
        {
            CourseVm courseVm = new CourseVm();
            courseVm.CourseDtos = new List<CourseDto>(); // Initialize the list if it's not already initialized

            var res = await _courseService.GetByIdAsync(id);
            if (res.Data != null && res.Success)
                courseVm.CourseDtos.Add(new CourseDto
                {
                    CourseFullName = res.Data.CourseFullName,
                    CourseId = res.Data.CourseId,
                    CourseShortName = res.Data.CourseShortName,
                    Duration = res.Data.Duration,
                    CreatedBy = res.Data.CreatedBy,
                });

            return PartialView("_CoursePartialView", courseVm);
        }
        public async Task<IActionResult> CreateCourse(CourseVm courseVm)
        {
            APIResponse aPIResponse = new APIResponse();
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "opps" });
            }
            if (courseVm.CourseDtos != null && courseVm.CourseDtos.Where(x => x.CourseId == null).Count() >= 1)
            {
                var courseDtos = courseVm.CourseDtos.Where(x => x.CourseId == 0 || x.CourseId == null).Select(x => new CourseDto
                {
                    CourseFullName = x.CourseFullName,
                    CourseShortName = x.CourseShortName,
                    Duration = x.Duration,
                    CreatedBy = UserId,
                }).ToList();
                var result = await _courseService.CreateAsync(courseDtos);
                if (result.Success)
                {
                    aPIResponse.Success = result.Success;
                    aPIResponse.Message = result.Message;
                }
            }
            if (courseVm.CourseDtos != null && courseVm.CourseDtos.Where(x => x.CourseId != null).Count() == 1)
            {
                var courseEdit = courseVm.CourseDtos.Select(x => new CourseDto
                {
                    CourseFullName = x.CourseFullName,
                    CourseShortName = x.CourseShortName,
                    Duration = x.Duration,
                    CourseId = x.CourseId,
                    UpdatedBy = UserId
                }).First(x => x.CourseId != 0);
                var res = await _courseService.EditAsync(courseEdit);
            }

            return Json(aPIResponse);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
          Json(await _courseService.DeleteAsync(new CourseDto { CourseId = id }));
    }
}
