using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Services.IServices;
using Yesscc.Services.IServices;

namespace YES.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IContactUsService _contectUsServeice;
        private readonly ICourseEnquiryService _courseEnquiryService;
        public DashboardController(IContactUsService contactUsService, ICourseEnquiryService courseEnquiryService, ICurrentUserService currentUserService) : base(currentUserService)

        {
            _contectUsServeice = contactUsService;
            _courseEnquiryService = courseEnquiryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Enquiry()
        {
            var res = await _contectUsServeice.GetaAllAsync();
            if (res != null)
            {
                return View(res.Data);

            }

            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> CourseEnquiry()
        {
            var res = await _courseEnquiryService.GetAllAsync();
            if (res != null)
            {
                return View(res.Data);

            }

            return View(null);
        }

    }
}
