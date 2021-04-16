using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Cell : INotifyPropertyChanged
    {
        private int x;
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }
        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");
            }
        }
        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyPropertyChanged("Color");
            }
        }
        private bool isEmpty;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                NotifyPropertyChanged("IsEmpty");
            }
        }

        public Cell(int x, int y, string color, bool isEmpty)
        {
            X = x;
            Y = y;
            if (color == "red-piece")
                Color = "/Checkers;component/Resources/Sprites/red-piece.png";
            else if (color == "white-piece")
                Color = "/Checkers;component/Resources/Sprites/white-piece.png";
            else if (color == null)
                Color = "/Checkers;component/Resources/Sprites/empty.png";
            else
                Color = color;
            IsEmpty = isEmpty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
