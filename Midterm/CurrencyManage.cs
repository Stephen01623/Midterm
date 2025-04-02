﻿using System;
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
                Console.Write("Enter The CurrencyYou Want to Sell (e.g., BTC): ", System.Drawing.Color.Yellow);
                sellingFromCurrency = Console.ReadLine()?.ToUpper();
                Connection con = new Connection();

                //check if the currency exists
                if (conn.CheckCurrency(sellingFromCurrency))
                {

                    Console.Write("Enter the Amount you want to Sell: ", System.Drawing.Color.Yellow);
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

    }
}
