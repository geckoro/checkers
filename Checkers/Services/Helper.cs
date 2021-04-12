using Checkers.Models;
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
        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            string jsonString;
            ObservableCollection<ObservableCollection<Cell>> collection;

            try
            {
                jsonString = File.ReadAllText(@"..\..\Resources\Games\default.json");
                collection = JsonSerializer.Deserialize<ObservableCollection<ObservableCollection<Cell>>>(jsonString);
            }
            catch (IOException)
            {
                collection = new ObservableCollection<ObservableCollection<Cell>>();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            return collection;
        }
        public static ObservableCollection<ObservableCollection<Cell>> LoadGameBoard()
        {
            return null;
        }


        public static void SaveGameBoard(string name)
        {

        }
    }
}
