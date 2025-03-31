using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.Eventing.Reader;

namespace Midterm
{
    class user
    {
        public static bool isLoggedIn = false;
        public static Connection con = new Connection();
        public static bool conn = false;
        public static async void Register()
        {
            Console.WriteLine("Register");
            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();

            if (con.UserExists(email))
            {
                Console.WriteLine("Email already exists");
            }
            else
            {
                Console.WriteLine("Enter your username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                string password = Midterm.Password.ReadPassword();
                Console.WriteLine("Enter your phone number: ");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter your address: ");
                string address = Console.ReadLine();
                Midterm.Otp.SendOtp(email);

                DateTime otpExpiry = DateTime.Now.AddMinutes(1);
                while (!conn)
                {
                    if (DateTime.Now > otpExpiry)
                    {
                        Console.WriteLine("OTP expired! Please request a new one.");
                        return;
                    }

                    Console.WriteLine("Enter your OTP: ");
                    string otp = Console.ReadLine();
                    if (Midterm.Otp.VerifyOtp(email, otp))
                    {
                        con.InsertData(username, password, email, phone, address);
                        Console.WriteLine("Register Successfully");
                        conn = true;
                    }
                    else
                    {
                        Console.WriteLine("Otp Failed");
                    }
                }
            }
        }

        public static void Login()
        {
            Connection con = new Connection();
            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            string password = Midterm.Password.ReadPassword();
            Midterm.Otp.SendOtp(email);
            bool conn = false;

            DateTime otpExpiry = DateTime.Now.AddMinutes(1);
            while (!conn)
            {
                if (con.ValidateLogin(email, password))
                {
                    if (DateTime.Now > otpExpiry)
                    {
                        Console.WriteLine("OTP expired! Please request a new one.");
                        return;
                    }

                    Console.WriteLine("Enter your OTP: ");
                    string otp = Console.ReadLine();
                    if (Midterm.Otp.VerifyOtp(email, otp))
                    {
                        Midterm.Sendemail.Thank(email);
                        Console.WriteLine("Login Success");
                        conn = true;
                        isLoggedIn = true;
                    }
                    else
                    {
                        Console.WriteLine("Otp Failed");
                    }
                }
                else
                {
                    Console.WriteLine("Login Failed");
                    return;
                }
            }
        }
    }
}


   
