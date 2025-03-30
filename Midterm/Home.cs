using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Midterm
{
    class Home
    {
        public static void MainHome()
        {
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

                string[] options = [ "Register", "Login", "Exit" ];
                int selectedIndex = 0;

                
                int boxWidth = banner.Split('\n')[1].Length + 4; 
                int boxHeight = banner.Split('\n').Length + 6;  
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
                        Console.Write(bannerLines[i], Color.Blue);
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
                            Console.WriteLine(options[i], Color.White);
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

        public static void ExecuteOption(int index)
        {
            Console.Clear();
            switch (index)
            {
                case 0:
                    Midterm.user.Register();
                    break;
                case 1:
                    Midterm.user.Login();
                    break;
                case 2:
                    Console.WriteLine("Exiting...", Color.Red);
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine("\nPress any key to return to the menu...", Color.Red);
            Console.ReadKey();
            MainHome();
        }
    }
}
