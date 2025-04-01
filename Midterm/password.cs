using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Midterm
{
    public class Password
    {
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
      public static void HashPasswrod(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void passwordlenght(string password)
        {
            if (password.Length < 8)
            {
                Console.SetCursorPosition(1, 25);
                Console.WriteLine("Password must be at least 8 characters long.");
                Console.ReadKey();
                Midterm.Home.MainHome();
            }    
        }
        public static void ConfirmPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                Console.SetCursorPosition(1, 25);
                Console.WriteLine("Password does not match.");
                Console.ReadKey();
                Midterm.Home.MainHome();
            }
        }
        public static void Gmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.SetCursorPosition(1, 25);
                Console.WriteLine("Invalid email address.");
                Console.ReadKey();
                Midterm.Home.MainHome();
            }
        }

    }
}


