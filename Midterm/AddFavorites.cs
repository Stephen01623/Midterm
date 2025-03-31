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
     class AddFavorites
    {
        public static void AddFavoritePair()
        {
            Connection connection = new Connection();
            BinanceWebSocketClient client = new BinanceWebSocketClient();
            Console.WriteLine("List OF Available Pairs\n\n");
            foreach (var pair in client.getPairs())
            {
                Console.WriteLine(pair, Color.Blue);
            }
            
            

            while (true)
            {
                bool found = false;
                Console.Write("Enter Your Favorite Exchanges: ", Color.Green);
                string enteredPair = Console.ReadLine();

                string email = "charles.bernard.balaguer@student.pnm.edu.ph";
                {
                    foreach (var pair in client.getPairs())
                    {
                        if (pair == enteredPair)
                        {
                             found = true;
                            connection.InsertFavorites(enteredPair, email);
                            Console.WriteLine("Entered Pair Successfully Stored.");
                            break;
                        }

                    }
                    if (!found)
                    {
                        Console.WriteLine("\n\nPair Does not Exists.", Color.Red);
                    }
                     

                }
            }
                



        }
        }
}
