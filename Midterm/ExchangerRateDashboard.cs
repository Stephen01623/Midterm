﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Midterm
{
     class ExchangerRateDashboard
    {
        
        public static void EnterDashboard()
        {
            ExchangeDashboard();
        }
        public async static Task ExchangeDashboard()
        {
            
            
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            while (true)
            {
                Console.Clear();
                int consoleWidth = Console.WindowWidth;

                string banner = @" 
 ███╗   ███╗██╗   ██╗██╗  ████████╗██╗       ██████╗██╗   ██╗██████╗ ██████╗ ███████╗███╗   ██╗ ██████╗██╗   ██╗
 ████╗ ████║██║   ██║██║  ╚══██╔══╝██║      ██╔════╝██║   ██║██╔══██╗██╔══██╗██╔════╝████╗  ██║██╔════╝╚██╗ ██╔╝
 ██╔████╔██║██║   ██║██║     ██║   ██║█████╗██║     ██║   ██║██████╔╝██████╔╝█████╗  ██╔██╗ ██║██║      ╚████╔╝ 
 ██║╚██╔╝██║██║   ██║██║     ██║   ██║╚════╝██║     ██║   ██║██╔══██╗██╔══██╗██╔══╝  ██║╚██╗██║██║       ╚██╔╝  
 ██║ ╚═╝ ██║╚██████╔╝███████╗██║   ██║      ╚██████╗╚██████╔╝██║  ██║██║  ██║███████╗██║ ╚████║╚██████╗   ██║   
 ╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝       ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝   ╚═╝   
                                                                                                               
  ██████╗ ██████╗ ███╗   ██╗██╗   ██╗███████╗██████╗ ████████╗███████╗██████╗                                   
 ██╔════╝██╔═══██╗████╗  ██║██║   ██║██╔════╝██╔══██╗╚══██╔══╝██╔════╝██╔══██╗                                  
 ██║     ██║   ██║██╔██╗ ██║██║   ██║█████╗  ██████╔╝   ██║   █████╗  ██████╔╝                                  
 ██║     ██║   ██║██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██╔══██╗   ██║   ██╔══╝  ██╔══██╗                                  
 ╚██████╗╚██████╔╝██║ ╚████║ ╚████╔╝ ███████╗██║  ██║   ██║   ███████╗██║  ██║                                  
  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝                                   
";

                string[] options = ["Spot", "Swap", "Deposit", "Change Password", "Logout"];
                int selectedIndex = 0;


                int boxWidth = banner.Split('\n')[1].Length + 4;
                int boxHeight = banner.Split('\n').Length + 10;
                int boxX = (consoleWidth - boxWidth) / 2;
                int boxY = 3;

                while (true)
                {
                    Console.Clear();


                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write("╔" + new string('═', boxWidth - 2) + "╗");

                    for (int i = 1; i < boxHeight - 1; i++)
                    {
                        Console.SetCursorPosition(boxX, boxY + i);
                        Console.Write("║" + new string(' ', boxWidth - 2) + "║");
                    }

                    Console.SetCursorPosition(boxX, boxY + boxHeight - 1);
                    Console.Write("╚" + new string('═', boxWidth - 2) + "╝");


                    string[] bannerLines = banner.Split('\n');
                    int bannerStartY = boxY + 1;
                    for (int i = 0; i < bannerLines.Length; i++)
                    {
                        Console.SetCursorPosition(boxX + 2, bannerStartY + i);
                        Console.Write(bannerLines[i], Color.Pink);
                    }


                    int menuStartY = bannerStartY + bannerLines.Length + 1;
                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.SetCursorPosition(boxX + 5, menuStartY + i);

                        if (i == selectedIndex)
                        {
                            Console.Write(">> ", Color.Green);
                            Console.WriteLine(options[i], Color.LimeGreen);
                        }
                        else
                        {
                            Console.Write("   ");
                            Console.WriteLine(options[i], Color.Lavender);
                        }
                    }


                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Length;
                            break;
                        case ConsoleKey.Enter:
                            ExecuteOption(selectedIndex);
                            return;
                    }
                }
            }
            
            
        }

        public static async void LoobBack()
        {
            await ExchangeDashboard();
        }
        public static async void ExecuteOption(int index)
        {
           
            Console.Clear();
            switch (index)
            {
                case 0:
                    Midterm.ExchangeCaller.Call();
                    break;
                case 1:
                    Midterm.ExchangeCaller.Caller();
                    break;
                case 2:
                    Midterm.Deposit.InsertMoney();
                    break;
                case 3:
                    Midterm.updateuser.Changepassword();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                


            }

            Console.WriteLine("\nPress any key to return to the menu...", Color.Red);
            Console.ReadKey();
            await ExchangeDashboard();

        }

       
    }
}
