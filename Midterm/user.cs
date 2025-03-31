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
            //BinanceWebSocketClient client = new BinanceWebSocketClient();
            //await client.StartAsync();
            Console.WriteLine(@"
                         ██████╗ ███████╗ ██████╗ ██╗███████╗████████╗███████╗██████╗ 
                         ██╔══██╗██╔════╝██╔════╝ ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗
                         ██████╔╝█████╗  ██║  ███╗██║███████╗   ██║   █████╗  ██████╔╝
                         ██╔══██╗██╔══╝  ██║   ██║██║╚════██║   ██║   ██╔══╝  ██╔══██╗
                         ██║  ██║███████╗╚██████╔╝██║███████║   ██║   ███████╗██║  ██║
                         ╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝");
            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();
         
            if (con.UserExists(email))
            {
                Console.WriteLine("Email already exist");
            }
            else
            {
                Console.WriteLine(@"
 ___      ___  ___  __           __   ___  __                   ___  
|__  |\ |  |  |__  |__)    |  | /__` |__  |__) |\ |  /\   |\/| |__  .
|___ | \|  |  |___ |  \    \__/ .__/ |___ |  \ | \| /~~\  |  | |___ .");
                string username = Console.ReadLine(); 
                Console.WriteLine("Enter your password: ");
                string password = Midterm.Password.ReadPassword();
                Console.WriteLine("Enter your phone number: ");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter your address: ");
                string address = Console.ReadLine();
                Midterm.Otp.SendOtp(email);
                while (!conn)
                {
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
            Console.WriteLine(@" 

                                            ██╗      ██████╗  ██████╗ ██╗███╗   ██╗
                                            ██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
                                            ██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
                                            ██║     ██║   ██║██║   ██║██║██║╚██╗██║
                                            ███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
                                            ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝");
            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            string password = Midterm.Password.ReadPassword();
            Midterm.Otp.SendOtp(email);
            bool conn = false;
            while (!conn)
            {
                if (con.ValidateLogin(email, password))
                {
                    Console.WriteLine("Enter your OTP: ");
                    string otp = Console.ReadLine();
                    //if(Midterm.Otp.VerifyOtp)
                    if (Midterm.Otp.VerifyOtp(email, otp))
                    {
                        Midterm.Sendemail.Thank(email);
                        Console.WriteLine("Login Success");

                        //Dashboard of Cryptocurrency Exchange Rate
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
