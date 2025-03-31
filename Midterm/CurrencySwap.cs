using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Midterm;
using Console = Colorful.Console;


namespace Midterm
{
   
    class CurrencySwap
    {
        public static float amount_converted;
        public static string currency;
        private BinanceWebSocketClient client;
        private static Dictionary<string, float> exchangeRates;

        public CurrencySwap()
        {
            client = new BinanceWebSocketClient();
            exchangeRates = new Dictionary<string, float>();
        }

        public async Task Initialize()
        {
            await client.StartAsync();
            exchangeRates = client.GetTablePricing();
        }

        public async Task SwapCurrency()
        {
            Console.Clear();
            Console.WriteLine("💱 Cryptocurrency Swap System 💱", System.Drawing.Color.Cyan);
            Console.WriteLine("===================================", System.Drawing.Color.White);

            Console.Write("Enter the currency you want to swap (e.g., BTCUSDT): ", System.Drawing.Color.Yellow);
            string fromCurrency = Console.ReadLine()?.ToUpper();

            Console.Write("Enter the currency you want to receive (e.g., ETHUSDT): ", System.Drawing.Color.Yellow);
            string toCurrency = Console.ReadLine()?.ToUpper();

            Console.Write("Enter the amount you want to swap: ", System.Drawing.Color.Yellow);
            if (!float.TryParse(Console.ReadLine(), out float amount))
            {
                Console.WriteLine("❌ Invalid amount entered.", System.Drawing.Color.Red);
                return;
            }

            if (!exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
            {
                Console.WriteLine("❌ One or both currencies are not available.", System.Drawing.Color.Red);
                return;
            }

            float fromRate = exchangeRates[fromCurrency];
            float toRate = exchangeRates[toCurrency];

            float convertedAmount = (amount / fromRate) * toRate;

            Console.WriteLine($"✅ Successfully swapped {amount} {fromCurrency} to {convertedAmount:F6} {toCurrency}", System.Drawing.Color.Green);
            Console.WriteLine("===================================", System.Drawing.Color.White);
            Console.WriteLine("Press any key to return to the menu...", System.Drawing.Color.Red);
            Console.ReadKey();
        }
    }
}
