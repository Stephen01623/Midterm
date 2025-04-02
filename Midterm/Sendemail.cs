using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Midterm
{
     class Sendemail
    {
        static string senderEmail = "karyllen545@gmail.com";
        static string senderPassword = "jshw ychy ovru xdcj";
        public static void SendingemailOtp(string gmailA, string otp)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                string htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "email.html");
               

               
                string htmlBody = File.ReadAllText(htmlFilePath);
                
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "GROUP 1",
                    Body = htmlBody.Replace("{OTP}", otp),
                    IsBodyHtml = true
                };

                mail.To.Add(gmailA);


                client.Send(mail);

               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.Beep(1000, 2000);

            }
        }
        public static void Thank(string gmail)
        {
            try
            {

                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                string htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sendemail.html");



                string htmlBody = File.ReadAllText(htmlFilePath);


                

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Multi Currency Converter",
                    Body = htmlBody,
                    IsBodyHtml = true
                };

                mail.To.Add(gmail);


                client.Send(mail);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.Beep(1000, 2000);
            }
        }
    }
}
