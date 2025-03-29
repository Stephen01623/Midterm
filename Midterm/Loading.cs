using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class Loading
    {
        public static void ShowLoadingScreen()
        {
            Console.Clear();
            string loadingText = "Loading...";
            int consoleWidth = Console.WindowWidth;
            int textX = (consoleWidth - loadingText.Length) / 2;

            Console.SetCursorPosition(textX, Console.WindowHeight / 2 - 1);
            Console.Write(loadingText, Color.Cyan);

            
            int barWidth = 30;
            int barX = (consoleWidth - barWidth) / 2;
            Console.SetCursorPosition(barX, Console.WindowHeight / 2 + 1);
            Console.Write("[" + new string(' ', barWidth) + " ]", Color.White);

            for (int i = 0; i <= barWidth; i++)
            {
                Console.SetCursorPosition(barX + 1 + i, Console.WindowHeight / 2 + 1);
                Console.Write("█", Color.LimeGreen);
                Thread.Sleep(50); 
            }

            Thread.Sleep(500);
            Midterm.Home.MainHome();
        }
    }
}
