using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Activity;
namespace Midterm
{
    class FavoritesManager
    {
        public static void AddFavoritePair(string userEmail)
        {
            Connection connection = new Connection();
            BinanceWebSocketClient client = new BinanceWebSocketClient();
            Console.WriteLine("List of Available Pairs\n\n");

            foreach (var pair in client.getPairs())
            {
                Console.WriteLine(pair, Color.Blue);
            }

            while (true)
            {
                bool found = false;
                Console.Write("Enter Your Favorite Exchange: ", Color.Green);
                string enteredPair = Console.ReadLine();

                foreach (var pair in client.getPairs())
                {
                    if (pair == enteredPair)
                    {
                        found = true;
                        connection.InsertFavorites(enteredPair, userEmail);
                        Console.WriteLine("Entered Pair Successfully Stored.", Color.Yellow);
                        return;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("\n\nPair Does Not Exist.", Color.Red);
                }
            }
        }

        public static void ViewFavorites(string userEmail)
        {
            Connection connection = new Connection();
            List<string> favorites = connection.GetFavorites(userEmail);

            Console.WriteLine("\nYour Favorite Pairs:\n", Color.Cyan);
            if (favorites.Count == 0)
            {
                Console.WriteLine("No favorite pairs found.", Color.Red);
                return;
            }

            foreach (var pair in favorites)
            {
                Console.WriteLine(pair, Color.Green);
            }
        }

        public static void DeleteFavorite(string userEmail)
        {
            Connection connection = new Connection();
            List<string> favorites = connection.GetFavorites(userEmail);

            if (favorites.Count == 0)
            {
                Console.WriteLine("No favorite pairs to delete.", Color.Red);
                return;
            }

            Console.WriteLine("\nYour Favorite Pairs:\n", Color.Cyan);
            foreach (var pair in favorites)
            {
                Console.WriteLine(pair, Color.Green);
            }

            Console.Write("Enter the pair to remove: ", Color.Yellow);
            string pairToDelete = Console.ReadLine();

            if (favorites.Contains(pairToDelete))
            {
                connection.DeleteFavorite(pairToDelete, userEmail);
                Console.WriteLine("Favorite pair removed successfully.", Color.Yellow);
            }
            else
            {
                Console.WriteLine("Pair not found in your favorites.", Color.Red);
            }
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
