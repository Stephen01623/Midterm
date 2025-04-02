using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.WebSockets;
using Newtonsoft.Json.Linq; // Install via NuGet: Newtonsoft.Json
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Midterm;
using System.Text.RegularExpressions;
using Activity;


class BinanceWebSocketClient
{
    private static readonly string[] pairs = {
        "btcusdt", "ethusdt", "bnbusdt", "solusdt",
        "xrpusdt", "dogeusdt", "adausdt", "avaxusdt",
        "bchusdt", "dotusdt"
    };


    public string[] getPairs() { return pairs; }
    private readonly string wsUrl;

    private Dictionary<string, string> priceTable;


    public static string pair;
    public static string price;
    private static Dictionary<string, float> TablePricing;

    public Dictionary<string, float> GetTablePricing()
    {
        return TablePricing;
    }



    public BinanceWebSocketClient()
    {

        // Create WebSocket URL for multiple pairs
        wsUrl = "wss://stream.binance.com:9443/stream?streams=" +
            string.Join("/", Array.ConvertAll(pairs, pair => $"{pair}@trade"));

        // Initialize dictionary with default values
        priceTable = new Dictionary<string, string>();
        TablePricing = new Dictionary<string, float>();

        foreach (var pair in pairs)
        {
            priceTable[pair.ToUpper()] = "Waiting...";
        }
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Streaming to Binance Server...");
        try
        {
            bool exit = false;
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                Connection connect = new Connection();
                await ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
                Console.WriteLine("Connected to Binance Multi-Stream WebSocket!\n");

                byte[] buffer = new byte[4096];
                int displayCount = 0;
                
                while (ws.State == WebSocketState.Open)
                {


                    if (displayCount < 1000)
                    {
                        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        UpdatePriceTable(message);
                        DisplayPriceTable(); // Print all prices at once
                        
                        
                        displayCount++;
                    }
                    else

                    { 
                        Console.ReadKey();
                        ExchangerRateDashboard.selectedIndex = 0;
                       connect.UpdateAssetRate(GetTablePricing());

                       
                       
                    }


                    
                    
                }
                
            }
            if (exit)
            {
                Console.Write("Press Any Key: ");
                Console.ReadKey();
                await Midterm.Home.MainHome();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private void UpdatePriceTable(string jsonMessage)
    {
        try
        {

            var json = JObject.Parse(jsonMessage);
            pair = json["stream"].ToString().Split('@')[0].ToUpper(); // Extract pair name
            price = json["data"]["p"].ToString(); // Extract latest price
            float convertedPrice = float.Parse(price);
            TablePricing[pair] = convertedPrice;
            Console.WriteLine(convertedPrice);
            priceTable[pair] = price; // Store latest price

        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error parsing JSON: {ex.Message}");
        }
    }

    public void DisplayPriceTable()
    {
        Console.Clear();
        Console.WriteLine(@"

                             Top 10 Most Traded Crytocurrency From Binance
                                     ================================= 
                                        | Pair      |    Price     |
                                        |-----------|--------------|
");




        foreach (var pair in pairs)
        {

            Console.WriteLine(@$"                                        | {pair.ToUpper(),-10} | {priceTable[pair.ToUpper()],10} |");

        }


        Console.WriteLine(@"                                      =================================");
    }
}