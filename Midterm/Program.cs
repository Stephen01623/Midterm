using System;
using System.Text.RegularExpressions;
using Activity;

namespace Midterm
{
    class Program
    {
        static async Task Main()
        {

            //Midterm.Loading.ShowLoadingScreen();
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            //Midterm.AddFavorites.AddFavoritePair();
            //ExchangerRateDashboard.ExchangeDashboard();
            //Midterm.updateuser.Changepassword();

            //await client.StartAsync();
            //Midterm.updateuser.Changepassword();

            Connection conn = new Connection();
            Midterm.ExchangerRateDashboard.EnterDashboard();

            conn.GetRating("BTC", 1000);
        }
    }
    }

