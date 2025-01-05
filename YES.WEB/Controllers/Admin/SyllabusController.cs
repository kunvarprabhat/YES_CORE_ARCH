using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces; 
using YES.Dtos.Syllabus;
using YES.Services.IServices;

namespace YES.Controllers.Admin
{
    public class SyllabusController : BaseController
    {
        private readonly ISyllabusService _syllabusService;
        private readonly ICourseService _courseService;

        public SyllabusController(ISyllabusService syllabusService, ICourseService courseService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _syllabusService = syllabusService;
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll() =>
            Json(await _syllabusService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            SyllabusDto dto = new SyllabusDto();
            if (id != 0)
            {
                var res = await _syllabusService.GetByIdAsync(id);
                dto = res.Success && res.Data != null ? res.Data : new SyllabusDto();
            }
            var result = await _courseService.GetAllAsync();
            dto.Courses = result.Data;
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SyllabusDto syllabusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = syllabusDto.Id == null ? await _syllabusService.CreateAsync(syllabusDto)
                : await _syllabusService.EditAsync(syllabusDto);

            return Json(res);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
        Json(await _syllabusService.DeleteAsync(new SyllabusDto { Id = id }));

    }
}
