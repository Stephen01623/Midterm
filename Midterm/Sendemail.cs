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


                string htmlBody = @"
                <html>
                <head>
                    <style>
                        @keyframes highlight {
                            0% { background-color: yellow; color: black; }
                            100% { background-color: transparent; color: #007bff; }
                        }

                        body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }
                        .email-container { max-width: 600px; margin: auto; background: #fff; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }
                        .header { text-align: center; font-size: 24px; font-weight: bold; color: #333; }
                        .highlight { font-weight: bold; color: #007bff; animation: highlight 1.5s ease-in-out infinite alternate; padding: 4px; }
                        .content { font-size: 16px; color: #555; padding-top: 10px; line-height: 1.6; }
                        .footer { text-align: center; font-size: 12px; color: #777; padding-top: 10px; }
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='header'>Hello, This is Exchange-Currency</div>
                        <div class='content'>
                            <p>You are now Login in<span class='highlight'>Multi-currency converter</span> here in our application we convert.</p>
                            <p>money into different types of currency into your likings!</p>
                            <p>We highly appreciate you for testing our application</p>
                           
                            
                        </div>
                        <div class='footer'>Thank you for your time! 🚀</div>
                    </div>
                </body>
                </html>";

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

            }
        }
    }
}
