using System;
using System.Text.RegularExpressions;
using Activity;

namespace Midterm
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Multi-Currency Converter";
            //Midterm.Loading.ShowLoadingScreen();
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            
            //ExchangerRateDashboard.ExchangeDashboard();
            

            await client.StartAsync();
           
            //Connection conn = new Connection();
            //Midterm.ExchangerRateDashboard.EnterDashboard()
        }
    }
    }

