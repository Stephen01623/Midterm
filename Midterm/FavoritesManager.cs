using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Activity;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
namespace Midterm
{
    class FavoritesManager
    {
        public static Connection connection = new Connection();

        public static List<string> listOfFavorites = new List<string>();

        //public static MySqlConnection conn = new MySqlConnection(connection.GetConnectionString());
        
        
        public static bool CheckFavorites(string favoriteCurrency, string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection.GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT favorite_currency FROM favorites WHERE email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string listOfFavorites = reader.GetString("favorite_currency");

                                Console.Write($@"Your Favorites List is as Follows: {listOfFavorites}

");
                               
                                string[] parts = listOfFavorites.Split(new string[] { " - " }, StringSplitOptions.None);
                                foreach (string part in parts)
                                {
                                    if(part == favoriteCurrency.ToUpper())
                                    {
                                        Console.WriteLine("You Already Added this In your Favorites List");
                                        return false;
                                    }
                                    
                                }
                                
                                
                            } 
                        }         
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                Console.Beep(1000, 2000);
            }
            return true;

        }

        public static bool CheckEmailInFavorites(string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection.GetConnectionString()))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM favorites WHERE email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                Console.Beep(1000, 2000);
            }
            return false;
        }
        public static void UpdateFavorites(string favoriteCurrency, string email)
        {
            favoriteCurrency = favoriteCurrency.ToUpper();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection.GetConnectionString()))
                {
                    string query;
                    conn.Open();
                    if (!CheckEmailInFavorites(email))
                    {
                        query = "INSERT INTO favorites (email, favorite_currency) VALUES (@email, @favorite_currency)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@favorite_currency", favoriteCurrency);
                            object result = cmd.ExecuteNonQuery();
                        }

                        } else
                    {
                        query = "UPDATE favorites SET favorite_currency = CONCAT(favorite_currency, @favoriteCurrency) WHERE email = @email";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@favoriteCurrency", " - " + favoriteCurrency);
                            cmd.Parameters.AddWithValue("@email", email);
                            int rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                            if (rowsAffected > 0)
                            {

                                Console.WriteLine("Saving...");
                                Thread.Sleep(300);

                            }
                        }


                    }
                    Thread.Sleep(1000);
                    
                    
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
        }
        public static void AddFavoritePair(string userEmail)
        {
            Connection connection = new Connection();
            BinanceWebSocketClient client = new BinanceWebSocketClient();
            Console.WriteLine("List of Available Pairs\n\n");
            connection.DisplayCurrencies();
            while (true)
            {
                
                Console.Write("Enter Your Favorite Currency To pair With USDT: ", Color.Green);
                string favoriteCurrency = Console.ReadLine();

                if (connection.CheckCurrency(favoriteCurrency))
                {
                    if (CheckFavorites(favoriteCurrency, userEmail))
                    {
                        UpdateFavorites(favoriteCurrency, userEmail);
                        Console.WriteLine("Entered Pair Successfully Stored.", Color.Yellow);
                        Console.ReadKey();
                        return;
                    }
                   
                } else
                {
                    Console.WriteLine("\n\nPair Does Not Exist.", Color.Red);
                }            
                   
            }
        }
        public static void ViewFavorites(string userEmail)
        {
            listOfFavorites.Clear(); 
            Connection connection = new Connection();
            connection.GetFavorites(userEmail);
            Console.WriteLine("\nYour Favorite Pairs:\n", Color.Cyan);
            if (listOfFavorites.Count == 0)
            {
                Console.WriteLine("No favorite pairs found.", Color.Red);
                Thread.Sleep(2000);
                return;
            }
            Console.WriteLine("Fetching Data\n");
            Thread.Sleep(2000);
            foreach (var part in listOfFavorites)
            {
                Console.WriteLine(part, Color.Green);
            }
        }

        public static void DeleteFavorite(string userEmail)
        {
            
            Connection connection = new Connection();
            //List<string> favorites = connection.GetFavorites(userEmail);
            ViewFavorites(userEmail);
            if (listOfFavorites.Count == 0)
            {
                Console.WriteLine("No favorite pairs to delete.", Color.Red);
                Console.ReadKey();
                //return;
            }
           
            Console.Write("Enter the pair to remove: ", Color.Yellow);
            string pairToDelete = Console.ReadLine();

          
                connection.DeleteFavorite(pairToDelete, userEmail);
                Console.WriteLine("Favorite pair removed successfully.", Color.Yellow);
        }
        public static void FavoritesMenu(string userEmail)
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Favorites Menu\n", Color.Cyan);
                Console.WriteLine("1. View Favorites", Color.Green);
                Console.WriteLine("2. Add Favorite", Color.Green);
                Console.WriteLine("3. Delete Favorite", Color.Green);
                Console.WriteLine("4. Back to Main Menu", Color.Red);
                Console.Write("\nChoose an option: ", Color.Yellow);
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ViewFavorites(userEmail);
                        break;
                    case "2":
                        AddFavoritePair(userEmail);
                        break;
                    case "3":
                        DeleteFavorite(userEmail);
                        break;
                    case "4":
                        return; // Exit to main menu
                    default:
                        Console.WriteLine("Invalid choice. Try again.", Color.Red);
                        break;
                }
            }
        }
    }
}
