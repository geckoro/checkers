using Checkers.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkers.Services
{
    class Helper
    {
        public static Cell CurrentCell { get; set; }
        public static Cell PreviousCell { get; set; }

        public static Game InitGame()
        {
            string jsonString;
            Game game;

            try
            {
                jsonString = File.ReadAllText(@"..\..\Resources\Games\default.json");
                game = JsonSerializer.Deserialize<Game>(jsonString);
            }
            catch (IOException)
            {
                game = new Game();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            return game;
        }

        public static Game LoadGame(string path)
        {
            string jsonString;
            Game game;

            try
            {
                jsonString = File.ReadAllText(path);
                game = JsonSerializer.Deserialize<Game>(jsonString);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            return game;
        }

        public static void UpdateStatistics(string winner)
        {
            string jsonString;
            Statistics statistics;
            try
            {
                jsonString = File.ReadAllText(@"..\..\Resources\Games\statistics.json");
                statistics = JsonSerializer.Deserialize<Statistics>(jsonString);

                if (winner == "Red")
                {
                    statistics.RedPlayers++;
                }
                else
                {
                    statistics.WhitePlayers++;
                }

                jsonString = JsonSerializer.Serialize(statistics);
                File.WriteAllText(@"..\..\Resources\Games\statistics.json", jsonString);
            }
            catch (IOException)
            {
                statistics = new Statistics();

                if (winner == "Red")
                {
                    statistics.RedPlayers++;
                }
                else
                {
                    statistics.WhitePlayers++;
                }

                jsonString = JsonSerializer.Serialize(statistics);
                File.WriteAllText(@"..\..\Resources\Games\statistics.json", jsonString);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
