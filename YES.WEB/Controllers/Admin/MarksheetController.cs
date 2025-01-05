using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.Result;
using YES.Services.IServices;
using YES.Utility.Common;
using YES.Dtos;

namespace YES.Controllers.Admin
{
    public class MarksheetController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly IResultCertificateService _resultCertificateService;
        public MarksheetController(IStudentService studentService, IResultCertificateService resultCertificate, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _studentService = studentService;
            _resultCertificateService = resultCertificate;
        }
        [HttpGet("/Marksheet")]
        public async Task<IActionResult> GenerateMarksheet(int id)
        {
            var res = await _studentService.GetProfileById(id);
            return View(res.Data);

        }
        public async Task<IActionResult> UpdateDate(ResultCertificateDto certificateDto)
        {
            certificateDto.CreatedBy = UserId;
            certificateDto.UpdatedBy = UserId;
            certificateDto.UpdatedDate = DateTime.UtcNow;
            var res = await _resultCertificateService.UpdateMarksheetIssueDate(certificateDto);
            return Json(res);
        }

        [HttpPost]
        public IActionResult GeneratePDF([FromBody] PdfData data)
        {

            if (string.IsNullOrEmpty(data.Html))
            {
                return Json(new APIResponse
                {
                    Message = "Sorry? somting worng",
                    Status = System.Net.HttpStatusCode.OK,
                    Success = false
                });
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 10f, 10f, 20f, 0f))
                {
                    using (var writer = PdfWriter.GetInstance(document, memoryStream))
                    {
                        document.Open();

                        using (var stringReader = new StringReader(data.Html))
                        {
                            HTMLWorker htmlparser = new HTMLWorker(document);
                            htmlparser.Parse(stringReader);
                        }

                        // Add images and other content as needed
                        // Example:
                        string logopathRX = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "marksheet1.png");
                        Image img2 = Image.GetInstance(logopathRX);
                        img2.SetAbsolutePosition(0f, 0f);
                        img2.ScaleAbsolute(1500f, 0f);
                        img2.ScalePercent(0.5f * 24f);
                        document.Add(img2);

                        string StudentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", data.ImagePath);
                        //string StudentImage = data.ImagePath;
                        Image img5 = Image.GetInstance(StudentImage);
                        img5.SetAbsolutePosition(455f, 570f);
                        img5.ScaleAbsolute(1500f, 0f);
                        img5.ScalePercent(0.5f * 80);
                        document.Add(img5);

                        string AuthSign = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "sonusig-1.png");
                        Image img3 = Image.GetInstance(AuthSign);
                        img3.SetAbsolutePosition(470f, data.ResultCount == 2 ? 295f : data.ResultCount == 3 ? 265f : 235f);
                        img3.ScaleAbsolute(1500f, data.ResultCount == 2 ? 70f : data.ResultCount == 2 ? 40f : 40f);
                        img3.ScalePercent(0.5f * 100);
                        document.Add(img3);
                        string AuthSign1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", data.SignaturePath.Split("\\")[2]);
                        Image AuthImg = Image.GetInstance(AuthSign1);
                        if (data.BranchType == 0)
                        {
                            AuthImg.SetAbsolutePosition(380f, data.ResultCount == 2 ? 290f : data.ResultCount == 3 ? 260f : 235f);
                            AuthImg.ScaleAbsolute(1500f, data.ResultCount == 2 ? 20f : data.ResultCount == 2 ? 50f : 50f);
                            AuthImg.ScalePercent(0.5f * 80);
                            document.Add(AuthImg);

                        }
                        else
                        {
                            AuthImg.SetAbsolutePosition(350f, data.ResultCount == 2 ? 290f : data.ResultCount == 3 ? 260f : 235f);
                            AuthImg.ScaleAbsolute(1500f, data.ResultCount == 2 ? 20f : data.ResultCount == 2 ? 50f : 50f);
                            AuthImg.ScalePercent(0.5f * 120);
                            document.Add(AuthImg);
                        }


                       //// string QrCodeImg = $"/result-verification?StudentId={data.StdId}".GenerateQRCode(data.StdId);
                       //// Image img4 = Image.GetInstance(QrCodeImg);
                       // img4.SetAbsolutePosition(25f, 55f);
                       // img4.ScaleAbsolute(1500f, 0f);
                       // img4.ScalePercent(0.5f * 28);
                       // document.Add(img4);

                       // string footerfix = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "footer.png");
                       // Image img6 = Image.GetInstance(footerfix);
                       // img6.SetAbsolutePosition(24.4f, 20f);
                       // img6.ScaleAbsolute(1500f, 0f);
                       // img6.ScalePercent(0.5f * 60);
                       // document.Add(img6);

                       // // Close the document
                       // document.Close();
                    }
                }
                memoryStream.ToArray().SavePdf(data.StdId);
                // Write the PDF content to the response

                return Json(new APIResponse<object>
                {
                    Data = new { fileName = data.StdId + ".pdf" },
                    Success = true,
                    Message = "Generated successfuly"
                });

                //return File(System.IO.File.ReadAllBytes(filePath), "application/pdf");
            }
        }
        public IActionResult GeneratePDF(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Doc", fileName);
            return File(System.IO.File.ReadAllBytes(filePath), "application/pdf");
        }
    }
}
