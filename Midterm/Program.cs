using System;

namespace Midterm
{
    class Program
    {
        static async Task Main()
        {
            //Midterm.Loading.ShowLoadingScreen();
            BinanceWebSocketClient client = new BinanceWebSocketClient();
            ExchangerRateDashboard.ExchangeDashboard();

            //await client.StartAsync();
        }
    }
}