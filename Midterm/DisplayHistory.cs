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

                    string query = "SELECT * from conversion_history";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {   
                        
                        //cmd.Parameters.AddWithValue("@email", email);
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
                                Console.WriteLine("Currency: " + reader["currency"]);
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

        public static void InsertToHistory(string email, float amount_converted, float balance, string currency)
        {
            Connection connection = new Connection();
            string connectString = connection.GetConnectionString();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    email = "charles.bernard.balaguer.@student.pnm.edu.ph";
                    amount_converted = 100.00f;
                    currency = "BTCUSDT";

                    conn.Open();

                    string query = "INSERT INTO conversion_history (email, amount_converted, balance, currency, date) VALUES (@email, @amount_converted, @balance, @currency, @date)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@amount_converted", amount_converted);
                        cmd.Parameters.AddWithValue("@balance", balance);
                        cmd.Parameters.AddWithValue("@currency", currency);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine(@"View History");
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
