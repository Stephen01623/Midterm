using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Activity;
using Midterm;
using Console = Colorful.Console;


namespace Midterm
{
   
    class CurrencyManage
    {
        public static float amount_converted;
        public static string fromCurrency;
        public static string toCurrency;

        public static string sellingFromCurrency;
        public static BinanceWebSocketClient client = new BinanceWebSocketClient();
        private static Dictionary<string, float> exchangeRates;
        public static Connection conn = new Connection();
        public CurrencyManage()
        {
            client = new BinanceWebSocketClient();
            exchangeRates = new Dictionary<string, float>();
        }
        

        public async Task Initialize()
        {
            await client.StartAsync();
            exchangeRates = client.GetTablePricing();
        }

        public static async Task SwapCurrency()
        {
          
            Console.Clear();
            Console.WriteLine("Swap Currency ", System.Drawing.Color.Cyan);
            Console.WriteLine("===================================", System.Drawing.Color.White);

            Console.Write("Enter the currency you want to swap (e.g., USDT): ", System.Drawing.Color.Yellow);
            fromCurrency = Console.ReadLine()?.ToUpper();

            Console.Write("Enter the currency you want to receive (e.g., BTC): ", System.Drawing.Color.Yellow);
            toCurrency = Console.ReadLine()?.ToUpper();

            Console.Write($"Enter the amount of {fromCurrency} you want to swap: ", System.Drawing.Color.Yellow);
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

        public static async Task BuyCurrency()
        {
            BinanceWebSocketClient client = new BinanceWebSocketClient();
           
            Connection connect = new Connection();
            //Enter the currency to be bought
            Console.WriteLine("Buy Currency ", System.Drawing.Color.Cyan);
            Console.WriteLine("===================================", System.Drawing.Color.White);

            while(true)
            {
                Console.Write("Enter the currency you want to Buy (e.g., BTC): ", System.Drawing.Color.Yellow);
                toCurrency = Console.ReadLine()?.ToUpper();



                //check if the currency exists
                if (conn.CheckCurrency(toCurrency))
                {
                    
                    foreach (var pair in client.getPairs())
                    {
                        Console.WriteLine($"| {pair.ToUpper(),-10} | |");
                    }
                    foreach (var table in client.GetTablePricing())
                    {
                        Console.WriteLine(table.ToString());
                    }
                    Console.Write("Enter the Amount you want to Buy: ", System.Drawing.Color.Yellow);
                    float amount = float.Parse(Console.ReadLine());
                   
                    conn.BuyingCurrency(toCurrency, amount, conn.GetUserId(user.email), conn.GetAssetId(toCurrency));
                    break;
                }
                else
                {
                    Console.WriteLine("Currency Does not Exists.");
                }
            }

        }
        public static async Task SellCurrency()
        {
            // Currency selling into USDT
            BinanceWebSocketClient client = new BinanceWebSocketClient();

            Connection connect = new Connection();
            //Enter the currency to be bought
            Console.WriteLine("Sell Currency ", System.Drawing.Color.Cyan);
            Console.WriteLine("===================================", System.Drawing.Color.White);

            while (true)
            {
                Console.Write("Enter The CurrencyYou Want to Sell (e.g., BTC): ", System.Drawing.Color.Yellow);
                sellingFromCurrency = Console.ReadLine()?.ToUpper();



                //check if the currency exists
                if (conn.CheckCurrency(sellingFromCurrency))
                {

                    Console.Write("Enter the Amount you want to Sell: ", System.Drawing.Color.Yellow);
                    float amount = float.Parse(Console.ReadLine());

                    conn.BuyingCurrency(sellingFromCurrency, amount, conn.GetUserId(user.email), conn.GetAssetId(sellingFromCurrency));
                    break;
                }
                else
                {
                    Console.WriteLine("Currency Does not Exists.");
                }
            }
        }
        
    }
}
