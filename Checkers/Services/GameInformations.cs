using Checkers.Models;
using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Services
{
    class GameInformations
    {
        public static void SwapPlayers()
        {
            if (GameVM.CurrentPlayer.Name == "Red")
            {
                GameVM.CurrentPlayer.Name = "White";
                if (GameVM.RedPlayerScore == 12)
                {
                    GameVM.Turn.Message = "Red player won the game";
                    Helper.UpdateStatistics("Red");
                    return;
                }
            }
            else
            {
                GameVM.CurrentPlayer.Name = "Red";
                if (GameVM.WhitePlayerScore == 12)
                {
                    GameVM.Turn.Message = "White player won the game";
                    Helper.UpdateStatistics("White");
                    return;
                }
            }
            GameVM.Turn.Message = $"{GameVM.CurrentPlayer.Name} player has to move";
        }

        public static void PieceCaptured()
        {
            if (GameVM.CurrentPlayer.Name == "Red")
            {
                GameVM.RedPlayerScore++;
            }
            else
            {
                GameVM.WhitePlayerScore++;
            }
            GameVM.Score.Message = $"RED {GameVM.RedPlayerScore}:{GameVM.WhitePlayerScore} WHITE";
        }

        public static ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board, GameBusinessLogic bl)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c.X, c.Y, c.Color, c.IsEmpty, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        public static ObservableCollection<ObservableCollection<Cell>> CellVMBoardToCellBoard(ObservableCollection<ObservableCollection<CellVM>> board)
        {
            ObservableCollection<ObservableCollection<Cell>> result = new ObservableCollection<ObservableCollection<Cell>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<Cell> line = new ObservableCollection<Cell>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    line.Add(board[i][j].SimpleCell);
                }
                result.Add(line);
            }
            return result;
        }

        public static void UpdateBoard(ObservableCollection<ObservableCollection<Cell>> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < collection[i].Count; j++)
                {
                    Cell c = collection[i][j];
                    GameVM.GameBoard[i][j].SimpleCell.X = c.X;
                    GameVM.GameBoard[i][j].SimpleCell.Y = c.Y;
                    GameVM.GameBoard[i][j].SimpleCell.Color = c.Color;
                    GameVM.GameBoard[i][j].SimpleCell.IsEmpty = c.IsEmpty;
                }
            }
        }


        public static void UpdateScore(string currentPlayer, int redPlayerScore, int whitePlayerScore)
        {
            GameVM.CurrentPlayer.Name = currentPlayer;
            GameVM.RedPlayerScore = redPlayerScore;
            GameVM.WhitePlayerScore = whitePlayerScore;
            GameVM.Score.Message = $"RED {GameVM.RedPlayerScore}:{GameVM.WhitePlayerScore} WHITE";
            GameVM.Turn.Message = $"{GameVM.CurrentPlayer.Name} player has to move";
        }
    }
}
