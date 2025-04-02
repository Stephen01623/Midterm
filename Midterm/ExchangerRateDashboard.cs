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
     class ExchangerRateDashboard
    {
        
        public static void EnterDashboard()
        {
            ExchangeDashboard();
        }


        public static int selectedIndex = 0;
        public async static Task ExchangeDashboard()
        {
            
            
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            while (true)
            {
                Console.Clear();
                int consoleWidth = Console.WindowWidth;

                string banner = @" 
▐▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌
▐                                                                                                                     ▌
▐                                                                                                                     ▌
▐                                                                                                                     ▌
▐   ███╗   ███╗██╗   ██╗██╗  ████████╗██╗       ██████╗██╗   ██╗██████╗ ██████╗ ███████╗███╗   ██╗ ██████╗██╗   ██╗   ▌
▐   ████╗ ████║██║   ██║██║  ╚══██╔══╝██║      ██╔════╝██║   ██║██╔══██╗██╔══██╗██╔════╝████╗  ██║██╔════╝╚██╗ ██╔╝   ▌
▐   ██╔████╔██║██║   ██║██║     ██║   ██║█████╗██║     ██║   ██║██████╔╝██████╔╝█████╗  ██╔██╗ ██║██║      ╚████╔╝    ▌
▐   ██║╚██╔╝██║██║   ██║██║     ██║   ██║╚════╝██║     ██║   ██║██╔══██╗██╔══██╗██╔══╝  ██║╚██╗██║██║       ╚██╔╝     ▌
▐   ██║ ╚═╝ ██║╚██████╔╝███████╗██║   ██║      ╚██████╗╚██████╔╝██║  ██║██║  ██║███████╗██║ ╚████║╚██████╗   ██║      ▌
▐   ╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝       ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝   ╚═╝      ▌
▐                                                                                                                     ▌
▐                   ██████╗ ██████╗ ███╗   ██╗██╗   ██╗███████╗██████╗ ████████╗███████╗██████╗                       ▌
▐                  ██╔════╝██╔═══██╗████╗  ██║██║   ██║██╔════╝██╔══██╗╚══██╔══╝██╔════╝██╔══██╗                      ▌
▐                  ██║     ██║   ██║██╔██╗ ██║██║   ██║█████╗  ██████╔╝   ██║   █████╗  ██████╔╝                      ▌
▐                  ██║     ██║   ██║██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██╔══██╗   ██║   ██╔══╝  ██╔══██╗                      ▌
▐                  ╚██████╗╚██████╔╝██║ ╚████║ ╚████╔╝ ███████╗██║  ██║   ██║   ███████╗██║  ██║                      ▌
▐                   ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝                      ▌
▐                                                                                                                     ▌
▐                                                                                                                     ▌
▐▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌▌";

                Console.WriteLine(banner, Color.LimeGreen);
                string[] options = ["Spot", "Trade", "Deposit", "Add Favorites", "View Conversion History", "Change Password", "Logout"];
                int selectedIndex = 0;


                int menuStartY = 22;
                int menuStartX = (consoleWidth / 2) - 55;

                while (true)
                {

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.SetCursorPosition(menuStartX - 3, menuStartY + i);
                        Console.Write(new string(' ', consoleWidth));
                    }


                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.SetCursorPosition(menuStartX, menuStartY + i);

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



        public static async void ExecuteOption(int index)
        {
           
            Console.Clear();
            switch (index)
            {
                case 0:
                    Midterm.ExchangeCaller.Call();
                    break;
                case 1:
                    //ManageAsset manage = new ManageAsset();
                    Midterm.ManageAsset.ManageAssetDashboard();
                    break;
                case 2:
                    Midterm.Deposit.InsertMoney(User.email);
                    break;
                case 3:
                    Midterm.FavoritesManager.FavoritesMenu(User.email);
                    break;
                case 4:
                    Midterm.DisplayHistory.DisplayConversions(User.email);
                     break;
                case 5:
                    Midterm.updateuser.Changepassword();
                    break;
                case 6:
                    Midterm.User.Logout();
                    break;
                


            }

            Console.WriteLine("\nPress any key to return to the menu...", Color.Red);
            Console.ReadKey();
             
            await ExchangeDashboard ();

        }

        

       
    }
}
