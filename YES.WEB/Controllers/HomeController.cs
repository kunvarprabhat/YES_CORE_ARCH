using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YES.Dtos.Course;
using YES.Dtos.Home;
using YES.Services.IServices;
using YES.WEB.Models;
using Yesscc.Services.IServices;

namespace YES.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactUsService _contectUsServeice;
        private readonly ICourseEnquiryService _courseEnquiryService;
        public HomeController(ILogger<HomeController> logger, IContactUsService contactUsService, ICourseEnquiryService courseEnquiryService)
        {
            _logger = logger;
            _contectUsServeice = contactUsService;
            _courseEnquiryService = courseEnquiryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [Route("about-us")]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        [Route("director-messages")]
        public IActionResult DirectorMessages()
        {
            return View();
        }
        [HttpGet]
        [Route("mission-visions")]
        public IActionResult MissionAndVision()
        {
            return View();
        }
        [HttpGet]
        [Route("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        [Route("contact")]
        public async Task<IActionResult> ContactUs(ContactUsDto contactUsDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(contactUsDto);
            }
            var rep = await _contectUsServeice.CreateAsync(contactUsDto);
            return Json(rep);
        }

        [Route("book-set")]
        public async Task<IActionResult> BookSet(CourseEnquiryDto courseEnquiryDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(courseEnquiryDto);
            }
            var rep = await _courseEnquiryService.CreateAsync(courseEnquiryDto);
            return Json(rep);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}