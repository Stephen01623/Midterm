using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Activity;
using Midterm;
using MySql.Data.MySqlClient;
using Console = Colorful.Console;


namespace Midterm
{
   
    class CurrencyManage
    {
        public static float amount_converted;
        public static float amount;
        public static float sellingAmount;
        public static string fromCurrency;
        public static string toCurrency;
        public static string mainCurrency = "TETHER";
        public static string mainCurrencySymbol = "USDT";
       
        
        public static string sellingFromCurrency;
        public static BinanceWebSocketClient client = new BinanceWebSocketClient();
        private static Dictionary<string, float> exchangeRates;
        public static Connection conn = new Connection();
        public CurrencyManage()
        {
            client = new BinanceWebSocketClient();
            exchangeRates = new Dictionary<string, float>();
        }       
        public static float GetRate(string ticker_symbol)
        {
            
            float price = 0;
            try
            {
                using (MySqlConnection conns = new MySqlConnection(conn.GetConnectionString()))
                {
                    conns.Open();

                    string query = "SELECT rate from assets WHERE ticker_symbol = @ticker_symbol";
                    using (MySqlCommand cmd = new MySqlCommand(query, conns))
                    {
                        cmd.Parameters.AddWithValue("@ticker_symbol", ticker_symbol);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    
                                    price = reader.GetFloat("rate");
                                    Thread.Sleep(2000);
                                    return price;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }

            return price;
        }
        public async Task Initialize()
        {
            await client.StartAsync();
            exchangeRates = client.GetTablePricing();
        }

        public static async Task SwapCurrency()
        {
            Connection connect = new Connection();
            
            Console.Clear();
            Console.WriteLine("Swap Currency ", System.Drawing.Color.Cyan);
            Console.WriteLine("===================================", System.Drawing.Color.White);

            connect.DisplayCurrencies();
            Console.Write("Enter the currency you want to swap (e.g., USDT): ", System.Drawing.Color.Yellow);
            fromCurrency = Console.ReadLine()?.ToUpper();

            Console.Write("Enter the currency you want to receive (e.g., BTC): ", System.Drawing.Color.Yellow);
            toCurrency = Console.ReadLine()?.ToUpper();

            Console.Write($"Enter the amount of {fromCurrency} you want to swap: ", System.Drawing.Color.Yellow);
            float amountOfCurrency = float.Parse(Console.ReadLine());
            try
            {
                if (conn.CheckCurrency(fromCurrency))
                {
                   
                    CalculateConversion(amountOfCurrency, fromCurrency, toCurrency, conn.GetUserId(User.email), conn.GetAssetId(toCurrency));

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
            
            Console.WriteLine("Press any key to return to the menu...", System.Drawing.Color.Red);
            Console.ReadKey();
        }

        public static async Task BuyCurrency()
        {
            BinanceWebSocketClient client = new BinanceWebSocketClient();
           
            Connection connect = new Connection();


            //Enter the currency to be bought
            Console.WriteLine("Buy Currency ", System.Drawing.Color.Cyan);
            connect.DisplayCurrencies();

            Console.WriteLine("===================================", System.Drawing.Color.White);

            while(true)
            {
                Console.Write("Enter the currency you want to Buy (e.g., BTC): ", System.Drawing.Color.Yellow);
                toCurrency = Console.ReadLine()?.ToUpper();
                //check if the currency exists
                if (conn.CheckCurrency(toCurrency))
                {

                    Console.WriteLine(@$"

The Price Of {toCurrency} is {GetRate(toCurrency)}

Your Balance is {connect.GetBalance(User.email)}

");

                    foreach (var pair in client.getPairs())
                    {
                        Console.WriteLine($"| {pair.ToUpper(),-10} | |");
                    }
                    foreach (var table in client.GetTablePricing())
                    {
                        Console.WriteLine(table.ToString());
                    }
                    Console.Write("Enter the Amount Of USDT: ", System.Drawing.Color.Yellow);
                    float amount = float.Parse(Console.ReadLine());
                   
                    conn.BuyingCurrency(toCurrency, amount, mainCurrencySymbol, conn.GetUserId(User.email), conn.GetAssetId(toCurrency));
                    break;
                }
                else
                {
                    Console.WriteLine("Currency Does not Exists.");
                    Console.Beep(1000, 2000);
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

            connect.DisplayCurrencies();
            Console.WriteLine("===================================", System.Drawing.Color.White);


            Console.WriteLine(@$"
                    Your Balance is {connect.GetBalance(User.email)}

");
            while (true)
            {
                Console.Write("Enter The Currency You Want to Sell (e.g., BTC): ", System.Drawing.Color.Yellow);
                sellingFromCurrency = Console.ReadLine()?.ToUpper();
                Connection con = new Connection();

                //check if the currency exists
                if (conn.CheckCurrency(sellingFromCurrency))
                {

                    Console.Write("Enter the Amount of USDT You want to get: ", System.Drawing.Color.Yellow);
                    amount = float.Parse(Console.ReadLine());
                    if (CheckQuantity(con.GetUserId(User.email), con.GetAssetId(sellingFromCurrency)))
                    {
                        conn.SellCurrency(sellingFromCurrency, mainCurrencySymbol, amount, conn.GetUserId(User.email), conn.GetAssetId(sellingFromCurrency));
                        break;
                    }
                    else
                    {
                        Console.WriteLine(@$"Insufficient {sellingFromCurrency}", System.Drawing.Color.Yellow);
                    }
                }
                else
                {
                    Console.WriteLine("Currency Does not Exists.");
                    Console.Beep(1000, 2000);
                }
            }
        }


        public static bool CheckQuantity(int userId, int asset_id)
        {
            try
            {
                using (MySqlConnection conns = new MySqlConnection(conn.GetConnectionString()))
                {
                    conns.Open();
                    string query = "SELECT quantity FROM holdings WHERE user_id = @user_id AND asset_id = @asset_id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conns))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@asset_id", asset_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                float quantity = reader.GetFloat("quantity");
                                Console.WriteLine(quantity);
                                Thread.Sleep(2000);

                                double epsilon = 1e-10;
                                if (Math.Abs(quantity) > epsilon && amount <= quantity)
                                {
                                    Console.WriteLine("Performed");
                                    return true;
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return false;
        }

        public static void CalculateConversion(float amount, string toConvert, string converted, int user_id, int asset_id)
        {
           
            float rate1 = GetRate(toConvert);
            float rate2 = GetRate(converted);
            float calculatedAmount = (amount * rate1) / rate2;
         

            try
            {
                using (MySqlConnection connection = new MySqlConnection(conn.GetConnectionString()))
                {
                    connection.Open();
                    string query = "INSERT INTO holdings (user_id, asset_id, quantity, ticker_sym) VALUES (@user_id, @asset_id, @quantity, @ticker_sym)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@asset_id", asset_id);
                        cmd.Parameters.AddWithValue("@quantity", calculatedAmount);
                        cmd.Parameters.AddWithValue("@ticker_sym", converted);
                        Console.WriteLine("Currency Converted Successfully!");
                        Object result = cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception e) 
            {

                Console.WriteLine("Error: " + e.Message);
            }

            
            
        }
    }
}
