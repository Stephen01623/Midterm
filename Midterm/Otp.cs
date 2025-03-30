using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class Otp
    {
        public static string Generateotp;

        public static void SendOtp(string email)
        {
            Generateotp = Generate();
            Sendemail.SendOtp(email, Generateotp);
        }
        public static string Generate()
        {
            Random random = new Random();
            return random.Next(10000, 99999). ToString();
        }
        public static bool VerifyOtp(string userOTP) 
        {
            return userOTP == Generateotp;
        }
    }
}
