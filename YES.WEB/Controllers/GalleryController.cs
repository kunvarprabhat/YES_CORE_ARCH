using Microsoft.AspNetCore.Mvc;
using YES.Utility.Enums;
using YES.Services.IServices;
using YES.Dtos.Gallery;
using YES.Comman;
using YES.Dtos;
using YES.Domain.EntityClasses;
using YES.Comman.Interfaces;
using Microsoft.AspNetCore.Authorization;
using YES.Utility.Constants;

namespace YES.Controllers
{
    public class GalleryController : BaseController
    {
        private readonly IGalleryService _galleryService;
        private readonly FileUploadSettings _settings;
        public GalleryController(IGalleryService galleryService, FileUploadSettings settings, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _galleryService = galleryService;
            _settings = settings;
        }
        [HttpGet("GalleryImages")]
        public IActionResult Index(string galleryType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GalleryDto galleryDto = new GalleryDto()
            {
                ImageType = galleryType
            };
            return View(galleryDto);
        }
         
        public async Task<IActionResult> GetAll()
        {
            var res = await _galleryService.GetAllAsync();
            if (res.Success)
            {
                return Json(res.Data);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryDto galleryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FileSystemViewModel vm = new FileSystemViewModel();
            vm.UploadSettings = _settings;
            if (galleryDto.ImageType == GalleryType.Slider.ToString())
            {
                vm.Height = Constants.SliderHeight;
                vm.Width = Constants.SliderWidth;
            }
            else
            {
                vm.Height = Constants.ProfileHeight;
                vm.Width = Constants.ProfileWidth;
            }
            vm.PrefixName = $"{galleryDto.ImageType}_";
            if (!ModelState.IsValid)
            {
                var response = new APIResponse
                {
                    Message = "Some vailidation are requeired",
                    Success = false
                };
                return Json(response);
            }

            if (galleryDto.Id != 0)
            {
                vm.FileToUpload = galleryDto.FileToUpload;
                galleryDto.ImagePath = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                galleryDto.ImageName = vm.FileInfo.FileName;
                var res = await _galleryService.EditAsync(galleryDto);
                return Json(res);
            }
            else
            {
                vm.FileToUpload = galleryDto.FileToUpload;
                galleryDto.ImagePath = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                galleryDto.ImageName = vm.FileInfo.FileName;

                var res = await _galleryService.CreateAsync(galleryDto);

                return Json(res);
            }

        }
        [AllowAnonymous]
        [HttpGet("/photo-gallery")]
        public async Task<IActionResult> PhotoGallery()
        {
            var res = await _galleryService.GetAll(GalleryType.PhotoGallery.ToString());
            if (res.Success)
            {
                return View(res.Data);
            }
            return View(null);
        }
        [AllowAnonymous]
        [HttpGet("/news-media-Gallery")]
        public async Task<IActionResult> NewsMediaGallery()
        {
            var res = await _galleryService.GetAll(GalleryType.NewMedia.ToString());
            return View(res.Data);
        }
        [AllowAnonymous]
        [HttpGet("/award-achievement-gallery")]
        public async Task<IActionResult> AwardAchievementGallery()
        {
            var res = await _galleryService.GetAll(GalleryType.Award.ToString());
            return View(res.Data);
        }

        [AllowAnonymous]
        [HttpGet("/slider-images")]
        public async Task<IActionResult> SilderImage()
        {
            var res = await _galleryService.GetAll(GalleryType.Slider.ToString());
            return PartialView("Views/Home/_SliderImages.cshtml", res.Data);
        }
        [AllowAnonymous]
        [HttpGet("/getAll")]
        public async Task<IActionResult> GetAllGalleryImages()
        {
            var res = await _galleryService.GetAllAsync();
            return PartialView("Views/Home/_SliderImages.cshtml", res.Data);
        }

    }
}
