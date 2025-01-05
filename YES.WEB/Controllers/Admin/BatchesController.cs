using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.Batches;
using YES.Services.IServices;

namespace YES.Controllers.Admin
{
    public class BatchesController : BaseController
    {
        private readonly IBatchesService _batchesService;
        private readonly ICourseService _courseService;
        public BatchesController(IBatchesService batchesService, ICourseService courseService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _batchesService = batchesService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            BatchDto batchDto = new BatchDto();
            var result = await _courseService.GetAllAsync();
            batchDto.Courses = result.Data;
            return View(batchDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
           Json(await _batchesService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> GetById(int id) =>
           Json(await _batchesService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BatchDto batchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = batchDto.Id == 0 ? await _batchesService.CreateAsync(batchDto)
                : await _batchesService.EditAsync(batchDto);

            return Json(res);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
          Json(await _batchesService.DeleteAsync(new BatchDto { Id = id }));

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUpcomingBtaches()
        {
            var res = await _batchesService.GetAllAsync();
            return PartialView("~/Views/Home/_UpcomingCoursesPartialView.cshtml", res.Data);
        }
    }
}
