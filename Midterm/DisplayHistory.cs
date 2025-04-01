using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity;
using MySql.Data.MySqlClient;

namespace Midterm
{
    class DisplayHistory
    {

        public static void DisplayConversions(string email)
        {
            
            Connection connection = new Connection();
            string connectString = connection.GetConnectionString();

            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    
                    conn.Open();
                    

                    //string query = "SELECT * FROM conversion_history WHERE email = @email";
                    //string query = "SELECT from_currency, to_currency, amount, converted_amount, conversion_date FROM conversion_history WHERE user_id=@userId ORDER BY conversion_date DESC";

                    string query = "SELECT * from conversion_history WHERE email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {   
                        
                        cmd.Parameters.AddWithValue("@email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No records found.");
                                return;
                            }

                            while (reader.Read())
                            {
                                
                                Console.WriteLine("ID: " + reader["id"]);
                                Console.WriteLine("Email: " + reader["email"]);
                                Console.WriteLine("Balance: " + reader["balance"]);
                                Console.WriteLine("Currency To Convert: " + reader["currency_to_convert"]);
                                Console.WriteLine("Currency Converted to: " + reader["currency_converted_to"]);
                                Console.WriteLine("Created At: " + reader["date"]);
                                Console.WriteLine("----------------------------------");
                            }
                        }
                    }

                    }
                }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void InsertToHistory(string email, float amount_converted, float balance, string fromCurrency, string toCurrency, string action)
        {
            Connection connection = new Connection();
            string connectString = connection.GetConnectionString();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                    {
                    
                    conn.Open();

                    string query = "INSERT INTO conversion_history (email, amount_converted, balance, currency_to_convert, currency_converted_to, action, date) VALUES (@email, @amount_converted, @balance, @fromCurrency, @toCurrency, @action, @date)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email.ToLower());
                        cmd.Parameters.AddWithValue("@amount_converted", amount_converted);
                        cmd.Parameters.AddWithValue("@balance", balance);
                        cmd.Parameters.AddWithValue("@fromCurrency", fromCurrency);
                        cmd.Parameters.AddWithValue("@toCurrency", toCurrency);
                        cmd.Parameters.AddWithValue("@action", action);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);

            }

        }

    }
}
