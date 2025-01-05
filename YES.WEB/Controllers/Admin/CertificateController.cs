using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using YES.Comman.Interfaces;
using YES.Dtos.Result;
using YES.Services.IServices;
using YES.Domain.EntityClasses;
using System.Text.RegularExpressions;
using YES.Utility.Common;

namespace YES.Controllers.Admin
{
    public class CertificateController : BaseController
    { 
        private readonly IStudentService _studentService; 
        private readonly IResultCertificateService _resultCertificateService;
        public CertificateController(IStudentService studentService, IResultCertificateService resultCertificate, 
            ICurrentUserService currentUserService) : base(currentUserService)
        {
            _studentService = studentService;
            _resultCertificateService = resultCertificate;  
        }
        [HttpGet("/Certificate")]
        public async Task<IActionResult> GenerateCertificate(int id)
        {
            var res = await _studentService.GetProfileById(id);
            return View(res.Data);

        }
        public async Task<IActionResult> UpdateDate(ResultCertificateDto certificateDto)
        {
            certificateDto.CreatedBy = UserId;
            certificateDto.UpdatedBy = UserId;
            certificateDto.UpdatedDate = DateTime.UtcNow;
            var res = await _resultCertificateService.UpdateCertificateIssueDate(certificateDto);
            return Json(res);
        }
        public async Task<IActionResult> DownloadRecipt([FromBody] string html)
        {
            string filename = "YES00000020";

            html = Regex.Replace(html, @"<img[^>]+>", "");

            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 10f, 10f, 20f, 0f))
                {
                    using (var writer = PdfWriter.GetInstance(document, memoryStream))
                    {
                        document.Open();
                        using (var stringReader = new StringReader(html))
                        {
                            HTMLWorker htmlparser = new HTMLWorker(document);
                            htmlparser.Parse(stringReader);
                        }

                        // Add images and other content as needed 

                        string AuthSign = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "sonusig-1.png");
                        Image img3 = Image.GetInstance(AuthSign);
                        img3.SetAbsolutePosition(470f, 255f);
                        img3.ScaleAbsolute(1500f, 30f);
                        img3.ScalePercent(0.5f * 100);
                        document.Add(img3);
                        string AuthSign1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Athuries.png");
                        Image AuthImg = Image.GetInstance(AuthSign1);
                        AuthImg.SetAbsolutePosition(390f, 250f);
                        AuthImg.ScaleAbsolute(1500f, 30f);
                        AuthImg.ScalePercent(0.5f * 80);
                        document.Add(AuthImg);
                        // Close the document
                        document.Close();
                    }

                }

                string folderPath = memoryStream.ToArray().SavePdf(filename);
                // Write the PDF content to the response
                return Json(folderPath);
            }

        }
    }
}


