using Microsoft.AspNetCore.Mvc;
using YES.Dtos;
using YES.Services.IServices;
using YES.Dtos.Branch;
using YES.Comman.Interfaces;
using YES.Comman;
using YES.Domain.EntityClasses;
using YES.Utility.Common;
using YES.Utility.Constants;

namespace YES.Controllers.Admin
{
    public class BranchController : BaseController
    {
        private readonly IBranchService _branchService;
        private readonly FileUploadSettings _settings;
        public BranchController(IBranchService branchService,
            FileUploadSettings fileUploadSettings, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _branchService = branchService;
            _settings = fileUploadSettings;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getAll()
        {
            var result = await _branchService.GetAllAsync();

            return Json(result.Data);
        }

        public async Task<IActionResult> Create(int id)
        {
            BranchDto branchDto = new BranchDto();

            if (id != 0)
            {
                var result = await _branchService.GetByIdAsync(id);
                return View(result.Data);
            }
            var res = await _branchService.GetAllAsync();
            int BranchId = res.Data.OrderByDescending(x => x.Id).First().Id;
            branchDto.CenterId = $"YES{DateTime.Now.Year % 100}CID" + BranchId.ToString().GenrateRegNumber(BranchId + 1);
            return View(branchDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BranchDto branchDto)
        {
            FileSystemViewModel vm = new FileSystemViewModel();
            vm.UploadSettings = _settings;
            vm.Height = Constants.ProfileHeight;
            vm.Width = Constants.ProfileWidth;
            vm.PrefixName = $"Branch_";
            if (!ModelState.IsValid)
            {
                var response = new APIResponse
                {
                    Message = "Some vailidation are requeired",
                    Success = false
                };
                return Json(response);
            }

            if (branchDto.Id != 0)
            {
                if (branchDto.FileToUpload != null)
                {
                    vm.FileToUpload = branchDto.FileToUpload;
                    branchDto.Profile = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                }
                if (branchDto.FileSignature != null)
                {
                    vm.FileToUpload = branchDto.FileSignature;
                    branchDto.Signature = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                }
                var res = await _branchService.EditAsync(branchDto);
                return Json(res);
            }
            else
            {
                if (branchDto.FileToUpload != null)
                {
                    vm.FileToUpload = branchDto.FileToUpload;
                    branchDto.Profile = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                }
                if (branchDto.Signature != null)
                {
                    vm.FileToUpload = branchDto.FileSignature;
                    branchDto.Signature = await vm.Save() ? "\\" + _settings.UploadFolder + "\\" + vm.FileInfo.FileName : "";
                }
                var res = await _branchService.CreateAsync(branchDto);

                return Json(res);
            }

        }

        [HttpGet]
        public async Task<IActionResult> BranchDetails(int id)
        {
            BranchDto branchDto = new BranchDto();
            var res = await _branchService.GetBranchDetailsAsync(id);
            return View(branchDto);
        }
    }
}
