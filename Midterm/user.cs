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
using System.Globalization;

namespace Midterm
{
    class User
    {
        //turn this false next time
        public static bool isLoggedIn = false;
        public static Connection con = new Connection();
        public static bool conn = false;
        public static string email = "mypetadoptionsystem@gmail.com";

        public static void DrawBox(int x, int y, int width, int height)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("╔" + new string('═', width) + "╗");
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("║" + new string(' ', width) + "║");
            }
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("╚" + new string('═', width) + "╝");
        }

        public static void Register()
        {
            Console.Clear();
            Console.Write(@"


                                            ░█▀▄░█▀▀░█▀▀░▀█▀░█▀▀░▀█▀░█▀▀░█▀▄
                                            ░█▀▄░█▀▀░█░█░░█░░▀▀█░░█░░█▀▀░█▀▄
                                            ░▀░▀░▀▀▀░▀▀▀░▀▀▀░▀▀▀░░▀░░▀▀▀░▀░▀ ");
            int boxWidth = 70;
            int boxHeight = 15;
            int startX = (Console.WindowWidth - boxWidth) / 2;
            int startY = (Console.WindowHeight - boxHeight) / 2;
            DrawBox(startX, startY, boxWidth, boxHeight);

         
            
            Console.SetCursorPosition(startX + 5, startY + 3);
            Console.Write("Enter your Email: ");
            Console.SetCursorPosition(startX + 23, startY + 3);
            string email = Console.ReadLine();
            Midterm.Password.Gmail(email);

            if (con.UserExists(email))
            {
                Console.SetCursorPosition(startX + 5, startY + 4);
                Console.Write("Email already exists!");
                return;
            }

            
            Console.SetCursorPosition(startX + 5, startY + 4);
            Console.Write("Enter your Username: ");
            string username = Console.ReadLine();

            Console.SetCursorPosition(startX + 5, startY + 5);
            Console.Write("Enter your Password: ");
            string password = Midterm.Password.ReadPassword();
            Midterm.Password.passwordlenght(password);
            Console.SetCursorPosition(startX + 5, startY + 6);
            Console.Write("Enter Your Password Again: ");
            string password1 = Midterm.Password.ReadPassword();
            Midterm.Password.ConfirmPassword(password, password1);
            

            Console.SetCursorPosition(startX + 5, startY + 7);
            Console.Write("Enter your Phone: ");
            string phone = Console.ReadLine();

            Console.SetCursorPosition(startX + 5, startY + 8);
            Console.Write("Enter your Address: ");
            string address = Console.ReadLine();

            
            while (!conn)
            {
                Console.SetCursorPosition(startX + 5, startY + 9);
                Console.Write("Enter your OTP: ");
                Midterm.Otp.SendOtp(email);
                string otp = Console.ReadLine();
                if (Midterm.Otp.VerifyOtp(email, otp))
                {
                    con.InsertData(username, password, email, phone, address);
                    Console.SetCursorPosition(startX + 2, startY + 10);
                    Console.Write("Registered Successfully!");
                    
                    conn = true;
                }
                else
                {
                    Console.SetCursorPosition(startX + 2, startY + 8);
                    Console.Write("OTP Failed!");
                }
            }
        }

        public static void Login()
        {
            Console.Clear();
            Console.WriteLine(@"
                                   

                                                ░█░░░█▀█░█▀▀░▀█▀░█▀█
                                                ░█░░░█░█░█░█░░█░░█░█
                                                ░▀▀▀░▀▀▀░▀▀▀░▀▀▀░▀░▀");
            int boxWidth = 70;
            int boxHeight = 15;
            int startX = (Console.WindowWidth - boxWidth) / 2;
            int startY = (Console.WindowHeight - boxHeight) / 2;
            DrawBox(startX, startY, boxWidth, boxHeight);

            Console.SetCursorPosition(startX + 5, startY + 3);
            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            Midterm.Password.Gmail(email);

            Console.SetCursorPosition(startX + 5, startY + 4);
            Console.Write("Enter your Password: ");
            string password = Midterm.Password.ReadPassword();

            if (con.ValidateLogin(email, password))
            {
                Console.SetCursorPosition(startX + 5, startY + 5);
                Console.Write("Enter your OTP: ");
                Midterm.Otp.SendOtp(email);
                string otp = Console.ReadLine();
                if (Midterm.Otp.VerifyOtp(email, otp))
                {
                    Midterm.Sendemail.Thank(email);
                    Console.SetCursorPosition(startX + 5, startY + 6);
                    Console.Write("Login Successful!");
                    isLoggedIn = true;
                    ExchangerRateDashboard.ExchangeDashboard();
                }
                else
                {
                    Console.SetCursorPosition(startX + 5, startY + 6);
                    Console.Write("OTP Failed!");
                }
            }
            else
            {
                Console.SetCursorPosition(startX + 5, startY + 6);
                Console.Write("Login Failed!");
            }
        }

        public static void Logout()
        {
            isLoggedIn = false;
            Midterm.Loading.ShowLoadingScreen();
            FavoritesManager.listOfFavorites.Clear();
        }
    }
}
