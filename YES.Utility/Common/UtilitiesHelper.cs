
using QRCoder;
using System.Drawing;
using System.Text;

namespace YES.Utility.Common
{
    public static class UtilitiesHelper
    {
        public static string Grad(this decimal value)
        {
            string Grad = string.Empty;
            switch (value)
            {
                case var val when val >= 85:
                    Grad = "A+";
                    break;
                case var val when val >= 70 && val <= 85:
                    Grad = "A";
                    break;
                case var val when val >= 55 && val <= 70:
                    Grad = "B";
                    break;
                case var val when val >= 30 && val <= 55:
                    Grad = "C";
                    break;
                default:
                    Grad = "D";
                    break;
            }
            return Grad;
        }
        public static string Performance(this decimal value)
        {
            string Performance = string.Empty;
            switch (value)
            {
                case var val when val >= 85:
                    Performance = "Excellent";
                    break;
                case var val when val >= 70 && val <= 85:
                    Performance = "Very Good";
                    break;
                case var val when val >= 55 && val <= 70:
                    Performance = "Good";
                    break;
                case var val when val >= 30 && val <= 55:
                    Performance = "Satisfactory";
                    break;
                default:
                    Performance = "Try Again";
                    break;
            }
            return Performance;
        }
        public static string GenrateRegNumber(this string digit, int id)
        {
            switch (digit.Count())
            {
                case 1:
                    digit = "0000" + id;
                    break;
                case 2:
                    digit = "000" + id;
                    break;
                case 3:
                    digit = "00" + id;
                    break;
                case 4:
                    digit = "0" + id;
                    break;
                default:
                    digit = id.ToString();
                    break;
            }
            return digit;
        }
        //public static string GenerateQRCode(this string URL)
        //{
        //    string url = "http://YES.in" + URL;
        //    QRCodeGenerator QrGenerator = new QRCodeGenerator();
        //    QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        //    QRCode QrCode = new QRCode(QrCodeInfo);
        //    using (Bitmap bitMap = QrCode.GetGraphic(20))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();

        //            return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(byteImage));
        //        }
        //    }
        //}
        //public static string GenerateQRCode(this string URL, string StdId)
        //{
        //    string url = "http://YES.in" + URL;
        //    QRCodeGenerator QrGenerator = new QRCodeGenerator();
        //    QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        //    QRCode QrCode = new QRCode(QrCodeInfo);
        //    using (Bitmap bitMap = QrCode.GetGraphic(20))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Qrcode");
        //            string path = Path.Combine(filePath, StdId + ".png");
        //            if (File.Exists(path))
        //            {
        //                // If file found, delete it
        //                File.Delete(path);
        //            }
        //            if (File.Exists(filePath))
        //            {
        //                // If file found, created it
        //                Directory.CreateDirectory(filePath);
        //            }
        //            File.WriteAllBytes(path, byteImage);
        //            return path;
        //            //return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(byteImage));
        //        }
        //    }
        //}
        public static string SavePdf(this byte[] byteImage, string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Doc");
            string path = Path.Combine(filePath, fileName + ".pdf");
            if (File.Exists(filePath))
            {
                // If file found, created it
                Directory.CreateDirectory(filePath);
            }
            if (File.Exists(path))
            {
                // If file found, delete it
                File.Delete(path);
            }
            File.WriteAllBytes(path, byteImage);
            return path;
        }
        public static string GenerateRandomPassword(int length)
        {
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string special = "!@#$%^&*()=<>?";
            string allChars = upper + lower + digits + special;

            Random random = new Random();
            var password = new StringBuilder(length);

            // Ensure at least one of each character type
            password.Append(GetRandomChar(upper, random));
            password.Append(GetRandomChar(lower, random));
            password.Append(GetRandomChar(digits, random));
            password.Append(GetRandomChar(special, random));

            // Generate the rest of the password
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the characters in the password
            string shuffledPassword = new string(password.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());

            return shuffledPassword;
        }
        private static char GetRandomChar(string chars, Random random)
        {
            return chars[random.Next(chars.Length)];
        }
    }
}
