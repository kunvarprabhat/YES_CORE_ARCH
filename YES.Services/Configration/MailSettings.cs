namespace YES.Services.Configration
{
    public class MailSettings
    {
        public string SmtpUsername { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpName { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSSL { get; set; }

    }
}
