﻿using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using Console = Colorful.Console;
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
                Console.WriteLine("Data inserted successfully!");
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
        public void InsertBalance(float balance, string email)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                   
                    string query = "UPDATE users SET balance = balance + @balance WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@balance", balance);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.ExecuteNonQuery();

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
            email = "charles.bernard.balaguer@student.pnm.edu.ph";
            string query = "SELECT balance FROM users WHERE email = @email";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    object result = cmd.ExecuteScalar();
            
                   
                }
            }

            return balance;
        }


        public void InsertFavorites(string favorite, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO favorites (email, favorites) VALUES (@email, @favorites)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@favorites", favorite);

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

        public List<string> GetFavorites(string email)
        {
            List<string> favorites = new List<string>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT favorites FROM favorites WHERE email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                favorites.Add(reader.GetString("favorites"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message, Color.Red);
            }

            return favorites;
        }


        public void DeleteFavorite(string favorite, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM favorites WHERE email = @Email AND favorites = @Favorite";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Favorite", favorite);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Favorite Pair Removed Successfully!", Color.Green);
                        }
                        else
                        {
                            Console.WriteLine("Pair Not Found in Favorites!", Color.Red);
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

        public void GetUserId(string email)
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
                            Console.WriteLine(userId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ", e.Message);
            }
        }
        public void GetAssetId(string ticker_symbol)
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
                        cmd.Parameters.AddWithValue("ticker_symbol", ticker_symbol);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            assetId = Convert.ToInt32(result);
                            Console.WriteLine(assetId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ", e.Message);
            }
        }
        public void BuyingCurrency(string desiredCurrency, float amount)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO holdings ";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ", e.Message);
            }
            
        }
    }
}



             
