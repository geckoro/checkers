using Checkers.Commands;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using Checkers.Models;
using System.Text.Json;

namespace Checkers.ViewModels
{
    class SettingsVM : BaseNotification
    {
        private SettingsLogic command;

        private bool multipleJumpsEnabled;
        public bool MultipleJumpsEnabled
        {
            get
            {
                return multipleJumpsEnabled;
            }
            set
            {
                multipleJumpsEnabled = value;
                NotifyPropertyChanged("MultipleJumpsEnabled");
            }
        }

        private string savesDirectoryPath;
        public string SavesDirectoryPath
        {
            get
            {
                return savesDirectoryPath;
            }
            set
            {
                savesDirectoryPath = value;
                NotifyPropertyChanged("SavesDirectoryPath");
            }
        }

        private Label scoreStatistics;
        public Label ScoreStatistics
        {
            get
            {
                return scoreStatistics;
            }
            set
            {
                scoreStatistics = value;
                NotifyPropertyChanged("ScoreStatistics");
            }
        }

        private ICommand browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    browseCommand = new RelayCommand<object>(command.Browse, param => true);
                }
                return browseCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand<object>(command.Save, param => true);
                }
                return saveCommand;
            }
        }

        public Settings Settings { get; set; }

        public SettingsVM()
        {
            command = new SettingsLogic(this);

            string jsonString = File.ReadAllText(@"..\..\Resources\config.json");
            Settings = JsonSerializer.Deserialize<Settings>(jsonString);

            MultipleJumpsEnabled = Settings.MultipleJumpsEnabled;
            SavesDirectoryPath = Settings.SavesDirectoryPath;

            jsonString = File.ReadAllText(@"..\..\Resources\Games\statistics.json");
            Statistics statistics = JsonSerializer.Deserialize<Statistics>(jsonString);

            ScoreStatistics = new Label($"Red players won {statistics.RedPlayers} games\nWhite players won {statistics.WhitePlayers} games");
        }
    }
}
