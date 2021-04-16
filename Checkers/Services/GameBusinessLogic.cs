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
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> cells;

        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            this.cells = cells;
        }

        public void Move(Cell currentCell)
        {
            Helper.CurrentCell = currentCell;
            if (MovesLogic.IsMoveValid())
            {
                if (!MovesLogic.MakeMove())
                    GameInformations.SwapPlayers();
                return;
            }
            MovesLogic.HighlightPiece(Helper.CurrentCell);
            if (Helper.PreviousCell != null)
            {
                MovesLogic.UnhighlightPiece(Helper.PreviousCell);
            }
            Helper.PreviousCell = currentCell;
        }

        public void ClickAction(Cell obj)
        {
            if (GameVM.CurrentPlayer.Name == "Red" && (MovesLogic.ColorPath[obj.Color] == "red-piece" || MovesLogic.ColorPath[obj.Color] == "red-king") ||
                GameVM.CurrentPlayer.Name == "White" && (MovesLogic.ColorPath[obj.Color] == "white-piece" || MovesLogic.ColorPath[obj.Color] == "white-king") ||
                obj.IsEmpty)
            {
                Move(obj);
            }
        }
    }
}
