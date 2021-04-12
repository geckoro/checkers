using Checkers.Models;
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

        private void TurnCard(Cell cell)
        {
            //cell.DisplayedImage = cell.HidenImage;
        }

        private void TurnCardBack(Cell cell)
        {
            //cell.DisplayedImage = "/MVVMPairs;component/Resources/init.png";
        }

        public void Move(Cell currentCell)
        {
            //Helper.CurrentCell = currentCell;
            //TurnCard(currentCell);
            //if (Helper.PreviousCell != null)
            //{
            //    TurnCardBack(Helper.PreviousCell);
            //}
            //Helper.PreviousCell = currentCell;
        }

        public void ClickAction(Cell obj)
        {
            Move(obj);
        }
    }
}
