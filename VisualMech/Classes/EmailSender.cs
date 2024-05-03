using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

namespace VisualMech.Classes
{
    public static class EmailSender
    {
        public static string OTP { get; set; }

        public static void SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    MailMessage message = new MailMessage(from, to)
                    {
                        Subject = subject,
                        Body = body,
                        BodyEncoding = Encoding.UTF8,
                        IsBodyHtml = true
                    };
                    System.Net.NetworkCredential basicCredential1 =
                        new System.Net.NetworkCredential(from, "cxrs ynjy mnns akxi");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;

                    client.Send(message);

        
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GenerateOTP()
        {
            int otpLength = 4;

            Random random = new Random();
            string otpDigits = new string(Enumerable.Repeat("0123456789", otpLength)
                                  .Select(s => s[random.Next(s.Length)]).ToArray());
            
            OTP = otpDigits;
        }

        public static void SendOTPEmail(string email, string subject, string startingBody)
        {
            GenerateOTP();
            string from = "dummytry935@gmail.com";
            string mailbody = startingBody + $". Your OTP is: {OTP}";

            SendEmail(from, email, subject, mailbody);
            
        }

        public static bool IsValidEmail(string input)
        {
            string pattern = @"^(?:\s*|(?:(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})\s*))$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(input);
        }

    }
}