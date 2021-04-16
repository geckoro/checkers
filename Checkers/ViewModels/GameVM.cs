using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.ObjectModel;

namespace Checkers.ViewModels
{
    class GameVM
    {
        private GameBusinessLogic bl;

        public static ObservableCollection<ObservableCollection<CellVM>> GameBoard { get; set; }
        public static Player CurrentPlayer { get; set; }
        public static Label Score { get; set; }
        public static Label Turn { get; set; }
        public static int RedPlayerScore { get; set; }
        public static int WhitePlayerScore { get; set; }

        public GameVM()
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGame().Board;
            bl = new GameBusinessLogic(board);

            GameBoard = GameInformations.CellBoardToCellVMBoard(board, bl);
            CurrentPlayer = new Player("Red");
            RedPlayerScore = 0;
            WhitePlayerScore = 0;
            Score = new Label($"RED {RedPlayerScore}:{WhitePlayerScore} WHITE");
            Turn = new Label($"{CurrentPlayer.Name} player has to move");
        }
    }
}
