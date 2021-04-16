using Checkers.Models;
using Checkers.ViewModels;
using Checkers.Views;
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
    class MenuItemLogic
    {
        private MenuItemVM menuItemVM;

        public MenuItemLogic(MenuItemVM menuItemVM)
        {
            this.menuItemVM = menuItemVM;
        }

        public void New(object obj)
        {
            ObservableCollection<ObservableCollection<Cell>> collection = Helper.InitGame().Board;
            GameInformations.UpdateBoard(collection);
            GameInformations.UpdateScore("Red", 0, 0);
        }

        public void Save(object obj)
        {
            Settings settings;
            string jsonString;

            try
            {
                jsonString = File.ReadAllText(@"..\..\Resources\config.json");
                settings = JsonSerializer.Deserialize<Settings>(jsonString);
            }
            catch (IOException)
            {
                settings = new Settings();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            Game game = new Game();
            game.Board = GameInformations.CellVMBoardToCellBoard(GameVM.GameBoard);
            game.PlayerToMove = GameVM.CurrentPlayer.Name;
            game.RedPlayerScore = GameVM.RedPlayerScore;
            game.WhitePlayerScore = GameVM.WhitePlayerScore;

            jsonString = JsonSerializer.Serialize(game);
            File.WriteAllText(settings.SavesDirectoryPath + "\\save-" +
                 DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json", jsonString);
        }

        public void Load(object obj)
        {
            Game game;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                game = Helper.LoadGame(openFileDialog.FileName);
                GameInformations.UpdateBoard(game.Board);
                GameInformations.UpdateScore(game.PlayerToMove, game.RedPlayerScore, game.WhitePlayerScore);
            }
        }

        public void Settings(object obj)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        public void About(object obj)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}
