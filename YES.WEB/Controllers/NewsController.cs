using Microsoft.AspNetCore.Mvc;
using YES.Dtos;
using YES.Dtos.Batches;
using YES.Dtos.News;
using YES.Services.IServices;
using YES.Services.Services;

namespace YES.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _news;
        public NewsController(INewsService news)
        {
            _news = news;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            NewsDto newsDto = new NewsDto();
            if (id != 0)
            {
                var res = await _news.GetByIdAsync(id);
                if (res != null && res.Data != null) newsDto = res.Data;

                return View(newsDto);

            }

            return View(newsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsDto newsDto)
        {
            if (!ModelState.IsValid)
            {
                var response = new APIResponse
                {
                    Message = "Some vailidation are requeired",
                    Success = false
                };
                return Json(response);
            }

            if (newsDto.Id != 0)
            {
                var res = await _news.EditAsync(newsDto);
                return Json(res);
            }
            else
            {
                var res = await _news.CreateAsync(newsDto);

                return Json(res);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
        Json(await _news.DeleteAsync(new NewsDto { Id = id }));
        public async Task<IActionResult> GetAll()
        {
            var res = await _news.GetAllAsync();
            return Json(res);

        }
        [Route("/latest-news")]
        public async Task<IActionResult> LatestNews()
        {
            var res = await _news.GetAllAsync();
            return View(res.Data);
        }
        public async Task<IActionResult> GetNewsAndUpdates()
        {
            var res = await _news.GetAllAsync();
            return PartialView("~/Views/Home/_NewsPartialView.cshtml", res.Data.Take(5));
        }
        [Route("/lastes-videos")]
        public IActionResult LastesVideos()
        {
            return View();
        }
    }
}
