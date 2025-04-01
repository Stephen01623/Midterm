using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity;
using Console = Colorful.Console;
namespace Midterm
{
    class updateuser
    {
       
        public static void Changepassword()
        {

            Console.WriteLine(@"
░█▀▀░█░█░█▀█░█▀█░█▀▀░█▀▀░░░█▀█░█▀█░█▀▀░█▀▀░█░█░█▀█░█▀▄░█▀▄
░█░░░█▀█░█▀█░█░█░█░█░█▀▀░░░█▀▀░█▀█░▀▀█░▀▀█░█▄█░█░█░█▀▄░█░█
░▀▀▀░▀░▀░▀░▀░▀░▀░▀▀▀░▀▀▀░░░▀░░░▀░▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░▀░▀░▀▀░", Color.AliceBlue);
            Connection conn = new Connection();
           Console.SetCursorPosition(5, 10);
            Console.WriteLine("Enter Email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter old Password");
            string oldpassword = Console.ReadLine();


            if (conn.ValidateLogin(email, oldpassword))
            {

                Console.WriteLine("Enter New Password");
                string newpassword = Console.ReadLine();
                conn.UpdatePassword(email, newpassword);
                Console.WriteLine("Change Password Succesfully");
                Console.ReadKey();


                Midterm.User.conn = true;

            }
            else
            {
                Console.WriteLine("Invalid Email or Password");
            }

        }
    }
}
