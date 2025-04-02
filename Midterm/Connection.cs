using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using Console = Colorful.Console;
using Midterm;
using System.Text.RegularExpressions;
using System.Numerics;
namespace Activity
{
    class Connection
    {
        protected string connectionString;

        public Connection()
        {
            string server = "localhost";
            string database = "multicurrency";
            string uid = "root";
            string password = "";
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
        }
        public string GetConnectionString()
        {
            return connectionString;

        }

        public void InsertData(string username, string userPassword, string useremail, string usercellphonenumber, string useraddress)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (UserName, Password, Email, Cellphonenumber, Address) VALUES (@UserName, @Password, @Email, @Cellphonenumber, @Address)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", userPassword);
                        cmd.Parameters.AddWithValue("@Email", useremail);
                        cmd.Parameters.AddWithValue("@Cellphonenumber", usercellphonenumber);
                        cmd.Parameters.AddWithValue("@Address", useraddress);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public bool ValidateLogin(string email, string userPassword)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", userPassword);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool UserExists(string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE users SET Password = @Password WHERE Email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }


        }
        public void InsertBalance(float moneyToInsert, string email)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE users SET balance = balance + @moneyToInsert WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@moneyToInsert", moneyToInsert);
                        cmd.Parameters.AddWithValue("@email", email);
                        object result=  cmd.ExecuteNonQuery();
                        int rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine(rowsAffected);
                        } else
                        {
                            Console.WriteLine("Hindi napasok");
                        }
                    } 
                }
                Console.WriteLine("Balance Deposited Successfully!");
                

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }

            


        }
        public float GetBalance(string email)
        {
            float balance = 0;
           
            string query = "SELECT balance FROM users WHERE email = @email";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        object result = cmd.ExecuteScalar();

                        if (result != null || result != DBNull.Value)
                        {

                            balance = Convert.ToInt32(result);
                            return balance;
                            
                        } else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
               
            }
            Console.ReadKey();

            return balance;
        }


        public void InsertFavorites(string favorite, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO favorites (email, favorite_currency) VALUES (@email, @favorites)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@favorite_currency", favorite);

                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Data inserted successfully!", Color.Blue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void GetFavorites(string email)
        {
            List<string> favorites = new List<string>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT favorite_currency FROM favorites WHERE email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fetchedFavorites = reader.GetString("favorite_currency");
                                string[] parts = fetchedFavorites.Split(new string[] { " - " }, StringSplitOptions.None);
                            
                                foreach (string part in parts)
                                {
                                    FavoritesManager.listOfFavorites.Add(part);
                                    //Console.WriteLine(FavoritesManager.listOfFavorites.ToString());

                                    
                                }
 
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, Color.Red);
            }

           
        }


        public void DeleteFavorite(string favorite, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    //string query = "DELETE FROM favorites WHERE email = @Email AND favorite_currency = @Favorite";
                    string selectQuery = "SELECT favorite_currency FROM favorites WHERE email = @email";
                    using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, connection))
                    {
                        selectCmd.Parameters.AddWithValue("@email", email);
                        object result = selectCmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string storedValue = result.ToString();
                            List<string> currencies = storedValue.Split(" - ").ToList();
                            currencies.Remove(favorite.ToUpper());  

                            string updatedValue = string.Join(" - ", currencies);

                            // Update the record in the database
                            string updateQuery = "UPDATE favorites SET favorite_currency = @updatedValue WHERE email = @email";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                            {
                                updateCmd.Parameters.AddWithValue("@updatedValue", updatedValue);
                                updateCmd.Parameters.AddWithValue("@email", email);
                                updateCmd.ExecuteNonQuery();
                                Console.WriteLine("Currency Successfully Deleted", Color.Red);
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, Color.Red);
            }
        }


        public void DisplayCurrencies()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM assets";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No records found.");
                                return;
                            }

                            while (reader.Read())
                            {

                                Console.WriteLine($@"{reader["asset_name"]} ({reader["ticker_symbol"]}) To ({CurrencyManage.mainCurrencySymbol})");

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, Color.Red);
            }
        }
        public bool CheckCurrency(string ticker)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM assets WHERE ticker_symbol = @ticker";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ticker", ticker);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }

                }

            }
            catch (Exception e)
            {
                
                Console.WriteLine("Error: " + e.Message, Color.Red);
                
            }
            return false;
        }

        public int GetUserId(string email)
        {
           
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    int userId;
                    string query = "SELECT id FROM users WHERE email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("email", email);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                            return userId;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ", e.Message);
            }
            return 0;
        }
        public int GetAssetId(string ticker_symbol)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    int assetId;
                    string query = "SELECT asset_id FROM assets WHERE ticker_symbol = @ticker_symbol";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ticker_symbol", ticker_symbol);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            assetId = Convert.ToInt32(result);
                            return assetId;

                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " +  e.Message);
            }
            return 0;
        }

        public bool CheckHoldings(int user_id, int asset_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT COUNT(*) FROM holdings WHERE user_id = @user_id AND asset_id = @asset_id";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@asset_id", asset_id);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Dito " + e.Message);
            }
            return false;
        }
        public void BuyingCurrency(string desiredCurrency,  float amount, string currency_to_convert, int user_id, int asset_id)
        {
            //rate = amount / the price
            //amount = rate * the price
            float calculatedAmount = amount / CurrencyManage.GetRate(desiredCurrency);
            Console.WriteLine($@"
You will Get {calculatedAmount} Worth of {desiredCurrency}
Confirm? (y/n) ");
            string confirm = Console.ReadLine();

            if (confirm == "y" || confirm == "Y")
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query;
                        connection.Open();
                        if (CheckHoldings(user_id, asset_id))
                        {
                            query = "UPDATE holdings SET quantity = quantity + @amount WHERE user_id = @user_id AND asset_id = @asset_id";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@user_id", user_id);
                                cmd.Parameters.AddWithValue("@asset_id", asset_id);
                                cmd.Parameters.AddWithValue("@amount", calculatedAmount);
                                object result = cmd.ExecuteNonQuery();
                                Console.WriteLine("Asset Successfully Bought");
                                Console.ReadKey();

                            }

                        }

                        else
                        {
                            query = "INSERT INTO holdings (user_id, asset_id, quantity, ticker_sym) VALUES (@user_id, @asset_id, @quantity, @ticker_sym)";

                            
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@user_id", user_id);
                                cmd.Parameters.AddWithValue("@asset_id", asset_id);
                                cmd.Parameters.AddWithValue("@quantity", calculatedAmount);
                                cmd.Parameters.AddWithValue("@ticker_sym", desiredCurrency);
                                object result = cmd.ExecuteNonQuery();
                                Console.WriteLine("Currency Successfully Bought");
                                Console.ReadKey();

                            }
                        }



                        string selectQuery = "SELECT id, email, balance from users WHERE id = @user_id";
                        string action = "BUY";
                        using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@user_Id", user_id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                string email = User.email;
                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("No records found.");
                                    return;
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        int id = reader.GetInt32("id");
                                        email = reader.GetString("email");
                                        float balance = reader.GetFloat("balance");
                                        Midterm.DisplayHistory.InsertToHistory(email, amount, balance, CurrencyManage.mainCurrencySymbol, desiredCurrency, action);
                                        Console.WriteLine("Saved To History");

                                    }
                                    Console.ReadKey();
                                    UpdateBalance(email, amount, action);

                                }

                            }



                        }

                    }
                    Console.WriteLine($"Successfully Bought The {desiredCurrency}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error " + e.Message);
                    Console.ReadKey();
                }
            } else
            {
                Console.WriteLine("Cancelled");
                ManageAsset.ManageAssetDashboard();
            }
           
            
        }


        // One method for inserting conversion history
        // method for selling currency
        // method for increasing usdt balance
        public void SellCurrency(string sellingCurrency, string mainCurrency, float amount, int user_id, int asset_id)
        {
            Console.WriteLine(CurrencyManage.amount);
            Console.ReadKey();

            float calculatedAmount = (amount / CurrencyManage.GetRate(sellingCurrency)) * CurrencyManage.GetRate(sellingCurrency);
            //float calculatedAmount = CurrencyManage.sellingAmount * (1 / CurrencyManage.GetRate(sellingCurrency));


            Console.WriteLine("Calculated is " + calculatedAmount);
            Console.ReadKey();
            Console.WriteLine($@"
You will Get {calculatedAmount} Worth of USDT
Confirm? (y/n) ");
            string confirm = Console.ReadLine();

            if (confirm == "y" || confirm == "Y")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE holdings SET quantity = quantity - @amount WHERE user_id = @user_id AND asset_id = @asset_id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@amount", calculatedAmount);
                            cmd.Parameters.AddWithValue("@user_id", user_id);
                            cmd.Parameters.AddWithValue("@asset_id", asset_id);

                            object result = cmd.ExecuteNonQuery();



                            if (result == null)
                            {
                                return;
                            }



                            Console.WriteLine("Asset Sold Successfully!");
                            Console.ReadKey();
                        }

                        string selectQuery = "SELECT id, email, balance from users WHERE id = @user_id";
                        string action = "SELL";
                        using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@user_Id", user_id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                string email = User.email;
                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("No records found.");
                                    return;
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        int id = reader.GetInt32("id");
                                        email = reader.GetString("email");
                                        float balance = reader.GetFloat("balance");
                                        Midterm.DisplayHistory.InsertToHistory(email, amount, balance, sellingCurrency, mainCurrency, action);
                                        Console.WriteLine("Saved To History");

                                    }
                                    Console.ReadKey();
                                    UpdateBalance(email, calculatedAmount, action);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error " + e.Message);
                    Console.ReadKey();
                }
            } else
            {
                Console.WriteLine("Cancelled...");
            }




                

        }

        public void UpdateBalance(string email, float amount, string action)
        {
            int newAmount = Convert.ToInt32(amount);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query;
                    conn.Open();
                    if(action == "BUY")
                    {
                         query = "UPDATE users SET balance = balance - @amount WHERE email = @email";

                    }else
                    {
                        query = "UPDATE users SET balance = balance + @amount WHERE email = @email";

                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount", newAmount);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.ExecuteNonQuery();
                        

                    }
                }Console.WriteLine("Balance Updated Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                Console.ReadKey();
            }
            
        }

        public void UpdateAssetRate(Dictionary<string, float>tablePrices)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var pair in tablePrices)
                    {
                        string symbol = pair.Key;
                        float value = pair.Value;

                        Match match = Regex.Match(symbol, @"^([a-zA-Z]+)(usdt)$", RegexOptions.IgnoreCase); 
                        if (match.Success)
                        {
                            string baseCurrency = match.Groups[1].Value.ToUpper();
                            string quoteCurrency = match.Groups[2].Value.ToUpper();
                            string query = "UPDATE assets SET rate = @rate WHERE ticker_symbol = @ticker_symbol";
                            
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@rate", value);
                                cmd.Parameters.AddWithValue("@ticker_symbol", baseCurrency);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    
                                }else
                                {
                                    Console.WriteLine("Failed Updating Asset");
                                }

                            }
                        }
                    }Console.WriteLine(@"                   All The Currency Has Been Fetched...");
                    Console.ReadKey();
                 ExchangerRateDashboard.ExchangeDashboard();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                Console.ReadKey();
            }
        }

        public float GetRating(string ticker, float amount)
        {
            float rating = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT rate FROM assets where ticker_symbol = @ticker";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ticker", ticker);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                           while (reader.Read())
                            {

                                float rateToCalculate = reader.GetFloat("rate");
                                float calculated =  amount / rateToCalculate;
                                Console.WriteLine(calculated);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                Console.ReadKey();
            }

            return rating;
        }
    }
}



             
