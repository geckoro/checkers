using Checkers.Models;
using Checkers.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkers.Services
{
    class SettingsLogic
    {
        private SettingsVM settingsVM;

        public SettingsLogic(SettingsVM settingsVM)
        {
            this.settingsVM = settingsVM;
        }

        public void Browse(object obj)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                settingsVM.SavesDirectoryPath = openFileDlg.SelectedPath;
            }
        }

        public void Save(object obj)
        {
            settingsVM.Settings.MultipleJumpsEnabled = settingsVM.MultipleJumpsEnabled;
            settingsVM.Settings.SavesDirectoryPath = settingsVM.SavesDirectoryPath;
            string jsonString = JsonSerializer.Serialize(settingsVM.Settings);
            File.WriteAllText(@"..\..\Resources\config.json", jsonString);
        }
    }
}
