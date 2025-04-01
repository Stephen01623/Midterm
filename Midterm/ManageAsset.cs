using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Activity;
using Console = Colorful.Console;

namespace Midterm
{
    class ManageAsset
    {
       
        public static int selectedIndex = 0;
        
        public static float balance = 0;
        public static async Task ManageAssetDashboard()
        {
            //Connection connection = new Connection();
            //balance = connection.GetBalance("charles.bernard.balaguer@student.pnm.edu.ph");
            while (true)
            {
                Console.Clear();
                int consoleWidth = Console.WindowWidth;

                string banner = $@" 
███╗   ███╗ █████╗ ███╗   ██╗ █████╗  ██████╗ ███████╗     █████╗ ███████╗███████╗███████╗████████╗
████╗ ████║██╔══██╗████╗  ██║██╔══██╗██╔════╝ ██╔════╝    ██╔══██╗██╔════╝██╔════╝██╔════╝╚══██╔══╝
██╔████╔██║███████║██╔██╗ ██║███████║██║  ███╗█████╗      ███████║███████╗███████╗█████╗     ██║   
██║╚██╔╝██║██╔══██║██║╚██╗██║██╔══██║██║   ██║██╔══╝      ██╔══██║╚════██║╚════██║██╔══╝     ██║   
██║ ╚═╝ ██║██║  ██║██║ ╚████║██║  ██║╚██████╔╝███████╗    ██║  ██║███████║███████║███████╗   ██║   
╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝    ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝   ╚═╝ 


    Available Balance: {balance} USDT
";

                string[] options = ["Buy", "Sell", "Swap", "Back"];
                


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
                    Console.Write("╚" + new string('═', boxWidth - 2) + "╝"); //ano yon kapag may key na pinindot?


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


        public static async void ExecuteOption(int index)
        {

            Console.Clear();
            switch (index)
            {
                case 0:
                    await Midterm.CurrencyManage.BuyCurrency();
                    break;
                case 1:
                    await  Midterm.CurrencyManage.SellCurrency();
                    break;
                case 2:
                    await Midterm.CurrencyManage.SwapCurrency();
                     
                    break;
                case 3:
                    ExchangerRateDashboard.ExchangeDashboard();
                    break;
                
            }
            _= ManageAssetDashboard();
        }
       
    }
}


