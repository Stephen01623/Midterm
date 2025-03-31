using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
     class ExchangeCaller
    {
        public static async Task Call()
        {
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            await client.StartAsync();
            await Midterm.ExchangerRateDashboard.ExchangeDashboard();
        }
        public static async Task Caller()
        {
            CurrencySwap swapper = new CurrencySwap();
            await swapper.SwapCurrency();

        }
    }
}
