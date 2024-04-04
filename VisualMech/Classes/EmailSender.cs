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

namespace VisualMech.Classes
{
    public static class EmailSender
    {
        public static string OTP { get; set; }

        public static async Task SendEmailAsync(string from, string to, string subject, string body)
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

                    await client.SendMailAsync(message);

        
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

        public static async Task SendOTPEmailAsync(string email)
        {
            GenerateOTP();
            string from = "dummytry935@gmail.com";
            string mailbody = $"Hello, your OTP is: {OTP}";

            await EmailSender.SendEmailAsync(from, email, "VGMech Account Activation", mailbody);
            
        }



    }
}