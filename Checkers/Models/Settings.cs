using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Settings
    {
        public bool MultipleJumpsEnabled { get; set; }
        public string SavesDirectoryPath { get; set; }

        public Settings()
        {
            MultipleJumpsEnabled = false;
            SavesDirectoryPath = @"..\..\Resources\Games";
        }
    }
}
