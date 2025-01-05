using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using YES.Dtos;
using YES.Services.ComunicationServices;
using YES.Services.Configration;
using YES.Dtos.Mail;
using YES.Dtos.API; 
using System.Net.Mime; 

namespace YES.Services.CommunicationServices
{
    public class Communication : ICommunication
    {
        private readonly MailSettings _emailSettings;

        public Communication(IOptions<MailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }
        public async Task<APIResponse> SendAsync(SendMailDto mailDto)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_emailSettings.SmtpUsername);
                mail.To.Add(mailDto.ToAddress);
                mail.Subject = mailDto.Subject;
                mail.Body = mailDto.Body;
                mail.IsBodyHtml = true; // Enable HTML content

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = _emailSettings.SmtpPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                    EnableSsl = true,
                };

                try
                {
                    await smtpClient.SendMailAsync(mail);

                    return new APIResponse
                    {
                        Message = "Email sent successfully.",
                        Success = true,
                        Status = HttpStatusCode.OK,
                    };
                }
                catch (Exception ex)
                {
                    return new APIResponse
                    {
                        Message = "Failed to send email",
                        Success = false,
                        Status = HttpStatusCode.InternalServerError,
                        Error = new APIException(ex.Message, ex)
                    };
                }
            }
        }

        public Task<APIResponse> SendBulkAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<APIResponse> SendEmailAsync(SendMailDto model)
        {
            Random rnd = new Random();
            string filePath = string.Empty;
            MailMessage message = null;
            Attachment attachment = null;
            string[] toAddressList = model.ToAddress.Split(','); 
            try
            {
                //if (model.AttachmentFile != null && model.AttachmentFile.Count > 0)//need to be tested properly
                //{
                //    filePath = Path.Combine(_hostingEnvironment.ContentRootPath, Constant.ContentEmailContentPath, Constant.SendEmailFolder);
                //    if (!Directory.Exists(filePath))
                //        Directory.CreateDirectory(filePath);

                //    if (model.AttachFileName.Count > 1)
                //    {
                //        char[] delimiterChars = { '-', ':', '.' };
                //        string fileName = Convert.ToString(DateTime.Now);
                //        string name = string.Empty;
                //        string[] splitWord = fileName.Split(delimiterChars);
                //        foreach (string Name in splitWord)
                //        {
                //            name += Name;
                //        }
                //        filePath = filePath + "\\Mulit_File_Folder" + name; // Creating Folder
                //        Directory.CreateDirectory(filePath);

                //        for (int i = 0; i < model.AttachmentFile.Count; i++)
                //        {
                //            string newFileName = filePath + "\\File_" + rnd.Next(999) + "_" + model.AttachFileName[i];
                //            File.WriteAllBytes(newFileName, model.AttachmentFile[i]);
                //        }

                //        string zipPath = filePath + ".zip";
                //        ZipFile.CreateFromDirectory(filePath, zipPath);
                //        DeleteFolder(filePath);
                //        filePath = filePath + ".zip";
                //    }
                //    else
                //    {
                //        for (int i = 0; i < model.AttachmentFile.Count; i++)
                //        {
                //            filePath = filePath + "\\Receipt_" + rnd.Next(999) + model.AttachFileName[i];
                //            File.WriteAllBytes(filePath, model.AttachmentFile[i]);
                //        }
                //    }

                //}

                MailAddress fromEmail = new MailAddress(_emailSettings.SmtpName, "YES");
                string subject = model.Subject;
                string body = model.Body;

                var smtp = new SmtpClient
                {
                    Host = _emailSettings.SmtpHost,
                    Port = _emailSettings.SmtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                    DeliveryFormat = SmtpDeliveryFormat.International,
                    TargetName = _emailSettings.SmtpName
                };


                foreach (var toEmailAddress in toAddressList)
                {
                    message = new MailMessage(fromEmail.Address, toEmailAddress.Trim())
                    {
                        Subject = subject,
                        IsBodyHtml = true,
                        Priority = MailPriority.Normal,
                        BodyEncoding = System.Text.Encoding.GetEncoding("utf-8")
                    };

                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(body, @"< (.|\n) *?>", string.Empty), null, "text/plain");
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    message.AlternateViews.Add(plainView);
                    message.AlternateViews.Add(htmlView);

                    if (filePath.Length > 0)
                    {
                        attachment = new Attachment(filePath, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(filePath);
                        disposition.ModificationDate = File.GetLastWriteTime(filePath);
                        disposition.ReadDate = File.GetLastAccessTime(filePath);
                        disposition.FileName = Path.GetFileName(filePath);
                        disposition.Size = new FileInfo(filePath).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        message.Attachments.Add(attachment);
                    }

                    //if (model.CcList != null)
                    //{
                    //    foreach (var item in model.CcList)
                    //    {
                    //        message.CC.Add(new MailAddress(item));
                    //    }
                    //}

                    await smtp.SendMailAsync(message);
                }

                return new APIResponse
                {
                    Message = "Email sent successfully.",
                    Success = true,
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    Message = "Failed to send email",
                    Success = false,
                    Status = HttpStatusCode.InternalServerError,
                    Error = new APIException(ex.Message, ex)
                };
            }
        }
    }
}
