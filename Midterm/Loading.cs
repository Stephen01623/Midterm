using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Midterm
{
    class Loading
    {
        public static void ShowLoadingScreen()
        {
            Console.Clear(); 
            string group = @"



                                     ██████╗ ██████╗  ██████╗ ██╗   ██╗██████╗      ██╗
                                    ██╔════╝ ██╔══██╗██╔═══██╗██║   ██║██╔══██╗    ███║
                                    ██║  ███╗██████╔╝██║   ██║██║   ██║██████╔╝    ╚██║
                                    ██║   ██║██╔══██╗██║   ██║██║   ██║██╔═══╝      ██║
                                    ╚██████╔╝██║  ██║╚██████╔╝╚██████╔╝██║          ██║
                                     ╚═════╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝          ╚═╝
                                                                                       ";
            Console.WriteLine(group, Color.Blue);
            string loadingText = "Loading...";
            int consoleWidth = Console.WindowWidth;
            int textX = (consoleWidth - loadingText.Length) / 2;

            Console.SetCursorPosition(textX, Console.WindowHeight / 2 - 1 );
            Console.Write(loadingText, Color.Cyan);

            
            int barWidth = 50;
            int barX = (consoleWidth - barWidth) / 2;
            Console.SetCursorPosition(barX, Console.WindowHeight / 2 + 1);
            Console.Write("[" + new string(' ', barWidth) + " ]", Color.White);
            Console.WriteLine(@"


                                           Leader: Balanguer, Charles Bernard
                                           Member: Gerente, Ralph Joed 
                                                   Luib, Mark Christian
                                                   Cabarles, Knives Benedict
                                                   Nicolas, Karylle
                                                   Reboltan, Marie Cris
                                                   Montebon, Reca Mae
                                                   Collantes, Mary Joy
                                                   San Antonio, Marc Robert");

            for (int i = 0; i <= barWidth; i++)
            {
                Console.SetCursorPosition(barX + 1 + i, Console.WindowHeight / 2 + 1);
                Console.Write("█", Color.LimeGreen);
                Thread.Sleep(100); 
            }
            Console.Beep(1000, 2000);

            Midterm.Home.MainHome();
        }

    }
}
