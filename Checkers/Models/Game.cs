using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Game
    {
        public ObservableCollection<ObservableCollection<Cell>> Board { get; set; }
        public int RedPlayerScore { get; set; }
        public int WhitePlayerScore { get; set; }
        public string PlayerToMove { get; set; }


        public Game()
        {
            Board = new ObservableCollection<ObservableCollection<Cell>>();
            RedPlayerScore = 0;
            WhitePlayerScore = 0;
            PlayerToMove = "Red";
        }
    }
}
