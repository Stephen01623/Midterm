using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity;

namespace Midterm
{
    class updateuser
    {
       
        public static void Changepassword()
        {


            Connection conn = new Connection();
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
