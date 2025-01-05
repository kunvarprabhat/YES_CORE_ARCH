using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.TopMessage;
using YES.Services.IServices;
namespace YES.Controllers.Admin
{
    public class TopMessageController : BaseController
    {
        public readonly ITopMessageService _topMessageService;
        public TopMessageController(ITopMessageService topMessageService, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _topMessageService = topMessageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopMessageDto topMessageDto)
        {
            var res = await _topMessageService.CreateAsync(topMessageDto);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _topMessageService.GetByIdAsync(id);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TopMessageDto topMessageDto)
        {
            var result = await _topMessageService.DeleteAsync(topMessageDto);
            return Json(result);
        }
        public async Task<IActionResult> UpdateStatus(TopMessageDto topMessageDto)
        {
            var result = await _topMessageService.DeleteAsync(topMessageDto);
            return Json(result);
        }
    }
}
