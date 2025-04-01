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
           
        }
    }
}
