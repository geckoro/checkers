using Checkers.Models;
using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Checkers.Services
{
    using static GameInformations;

    class MovesLogic
    {
        private static bool HasCaptured { get; set; } = false;
        public static Dictionary<string, string> ColorPath { get; set; } =
          new Dictionary<string, string>()
          {
                { "/Checkers;component/Resources/Sprites/red-piece.png", "red-piece"},
                { "/Checkers;component/Resources/Sprites/white-piece.png", "white-piece"},
                { "/Checkers;component/Resources/Sprites/red-king.png", "red-king"},
                { "/Checkers;component/Resources/Sprites/white-king.png", "white-king"},
                { "/Checkers;component/Resources/Sprites/red-piece-highlighted.png", "red-piece-highlighted"},
                { "/Checkers;component/Resources/Sprites/white-piece-highlighted.png", "white-piece-highlighted"},
                { "/Checkers;component/Resources/Sprites/red-king-highlighted.png", "red-king-highlighted"},
                { "/Checkers;component/Resources/Sprites/white-king-highlighted.png", "white-king-highlighted"},
                { "/Checkers;component/Resources/Sprites/empty.png", null},

          };

        public static bool IsMoveValid()
        {
            if (Helper.PreviousCell == null)
            {
                return false;
            }
            if (Helper.PreviousCell.IsEmpty)
            {
                return false;
            }
            if (Helper.CurrentCell == Helper.PreviousCell)
            {
                Helper.CurrentCell = null;
                Helper.PreviousCell = null;
                return false;
            }
            if (Helper.CurrentCell.Color == Helper.PreviousCell.Color)
            {
                return false;
            }

            bool result;
            result = IsCaptureValid();
            if (result)
            {
                PieceCaptured();
                HasCaptured = true;
                return true;
            }

            result = IsSimpleMoveValid();
            if (result)
            {
                HasCaptured = false;
                return true;
            }
            return false;
        }

        public static bool MakeMove()
        {
            Helper.CurrentCell.Color = Helper.PreviousCell.Color;
            Helper.CurrentCell.IsEmpty = false;
            Helper.PreviousCell.Color = ColorPath.FirstOrDefault(c => c.Value == null).Key;
            Helper.PreviousCell.IsEmpty = true;
            UnhighlightPiece(Helper.CurrentCell);

            if (Helper.CurrentCell.X == 0 || Helper.CurrentCell.X == 7)
            {
                CheckForPromotion();
            }

            if (IsMultipleJumpsEnabled())
            {
                if (CheckForLegalCapture(Helper.CurrentCell) && HasCaptured)
                {
                    return true;
                }
            }

            Helper.CurrentCell = null;
            Helper.PreviousCell = null;
            return false;
        }

        private static void CheckForPromotion()
        {
            if (ColorPath[Helper.CurrentCell.Color] == "red-piece")
            {
                Helper.CurrentCell.Color = ColorPath.FirstOrDefault(c => c.Value == "red-king").Key;
            }
            else if (ColorPath[Helper.CurrentCell.Color] == "white-piece")
            {
                Helper.CurrentCell.Color = ColorPath.FirstOrDefault(c => c.Value == "white-king").Key;
            }
        }

        private static bool IsSimpleMoveValid()
        {
            if (Helper.PreviousCell.Y != Helper.CurrentCell.Y + 1 && Helper.PreviousCell.Y != Helper.CurrentCell.Y - 1)
            {
                return false;
            }
            if (ColorPath[Helper.CurrentCell.Color] == "red-piece" || ColorPath[Helper.CurrentCell.Color] == "white-piece" ||
                ColorPath[Helper.CurrentCell.Color] == "red-king" || ColorPath[Helper.CurrentCell.Color] == "white-king")
            {
                return false;
            }
            if (ColorPath[Helper.PreviousCell.Color] == "red-piece-highlighted")
            {
                if (Helper.PreviousCell.X != Helper.CurrentCell.X + 1)
                {
                    return false;
                }
            }
            if (ColorPath[Helper.PreviousCell.Color] == "white-piece-highlighted")
            {
                if (Helper.PreviousCell.X != Helper.CurrentCell.X - 1)
                {
                    return false;
                }
            }
            if (ColorPath[Helper.PreviousCell.Color] == "red-king-highlighted" || ColorPath[Helper.PreviousCell.Color] == "white-king-highlighted")
            {
                if (Helper.PreviousCell.X != Helper.CurrentCell.X + 1 && Helper.PreviousCell.X != Helper.CurrentCell.X - 1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCaptureValid()
        {
            switch (ColorPath[Helper.PreviousCell.Color])
            {
                case "red-piece-highlighted":
                    if (!Helper.CurrentCell.IsEmpty)
                    {
                        return false;
                    }
                    if (Helper.PreviousCell.X != Helper.CurrentCell.X + 2)
                    {
                        return false;
                    }
                    if (Helper.CurrentCell.Y == Helper.PreviousCell.Y - 2)
                    {
                        if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 0, -1 }))
                        {
                            CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y - 1].SimpleCell);
                            return true;
                        }
                    }
                    else if (Helper.CurrentCell.Y == Helper.PreviousCell.Y + 2)
                    {
                        if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 0, 1 }))
                        {
                            CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y + 1].SimpleCell);
                            return true;
                        }
                    }
                    return false;

                case "white-piece-highlighted":
                    if (!Helper.CurrentCell.IsEmpty)
                    {
                        return false;
                    }
                    if (Helper.PreviousCell.X != Helper.CurrentCell.X - 2)
                    {
                        return false;
                    }
                    if (Helper.CurrentCell.Y == Helper.PreviousCell.Y - 2)
                    {
                        if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 0, -1 }))
                        {
                            CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y - 1].SimpleCell);
                            return true;
                        }
                    }
                    else if (Helper.CurrentCell.Y == Helper.PreviousCell.Y + 2)
                    {
                        if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 0, 1 }))
                        {
                            CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y + 1].SimpleCell);
                            return true;
                        }
                    }
                    return false;

                case "red-king-highlighted":
                    if (!Helper.CurrentCell.IsEmpty)
                    {
                        return false;
                    }
                    if (Helper.CurrentCell.Y == Helper.PreviousCell.Y - 2)
                    {
                        if (Helper.PreviousCell.X >= 2 && Helper.PreviousCell.X == Helper.CurrentCell.X + 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { -1, -1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y - 1].SimpleCell);
                                return true;
                            }
                        }
                        if (Helper.PreviousCell.X <= 5 && Helper.PreviousCell.X == Helper.CurrentCell.X - 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 1, -1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y - 1].SimpleCell);
                                return true;
                            }
                        }
                    }
                    else if (Helper.CurrentCell.Y == Helper.PreviousCell.Y + 2)
                    {
                        if (Helper.PreviousCell.X >= 2 && Helper.PreviousCell.X == Helper.CurrentCell.X + 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { -1, 1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y + 1].SimpleCell);
                                return true;
                            }
                        }
                        if (Helper.PreviousCell.X <= 5 && Helper.PreviousCell.X == Helper.CurrentCell.X - 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 1, 1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y + 1].SimpleCell);
                                return true;
                            }
                        }
                    }
                    return false;

                case "white-king-highlighted":
                    if (!Helper.CurrentCell.IsEmpty)
                    {
                        return false;
                    }
                    if (Helper.CurrentCell.Y == Helper.PreviousCell.Y - 2)
                    {
                        if (Helper.PreviousCell.X >= 2 && Helper.PreviousCell.X == Helper.CurrentCell.X + 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { -1, -1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y - 1].SimpleCell);
                                return true;
                            }
                        }
                        if (Helper.PreviousCell.X <= 5 && Helper.PreviousCell.X == Helper.CurrentCell.X - 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 1, -1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y - 1].SimpleCell);
                                return true;
                            }
                        }
                    }
                    else if (Helper.CurrentCell.Y == Helper.PreviousCell.Y + 2)
                    {
                        if (Helper.PreviousCell.X >= 2 && Helper.PreviousCell.X == Helper.CurrentCell.X + 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { -1, 1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X - 1][Helper.PreviousCell.Y + 1].SimpleCell);
                                return true;
                            }
                        }
                        if (Helper.PreviousCell.X <= 5 && Helper.PreviousCell.X == Helper.CurrentCell.X - 2)
                        {
                            if (CheckForNeighbour(Helper.PreviousCell, new int[2] { 1, 1 }))
                            {
                                CapturePiece(GameVM.GameBoard[Helper.PreviousCell.X + 1][Helper.PreviousCell.Y + 1].SimpleCell);
                                return true;
                            }
                        }
                    }
                    return false;
            }
            return false;
        }

        public static bool CheckForNeighbour(Cell cell, int[] direction)
        {
            if (cell.IsEmpty)
            {
                return false;
            }
            switch (ColorPath[cell.Color])
            {
                case "red-piece-highlighted":
                case "red-piece":
                    if (cell.X < 2)
                    {
                        return false;
                    }
                    if (cell.Y + direction[1] >= 1 && cell.Y + direction[1] <= 6)
                    {
                        if (GameVM.GameBoard[cell.X - 1][cell.Y + direction[1]].SimpleCell.IsEmpty)
                        {
                            return false;
                        }
                        if (ColorPath[GameVM.GameBoard[cell.X - 1][cell.Y + direction[1]].SimpleCell.Color].Equals("white-piece") ||
                            ColorPath[GameVM.GameBoard[cell.X - 1][cell.Y + direction[1]].SimpleCell.Color].Equals("white-king"))
                        {
                            return true;
                        }
                    }
                    return false;
                case "white-piece-highlighted":
                case "white-piece":
                    if (cell.X > 5)
                    {
                        return false;
                    }
                    if (cell.Y + direction[1] >= 1 && cell.Y + direction[1] <= 6)
                    {
                        if (GameVM.GameBoard[cell.X + 1][cell.Y + direction[1]].SimpleCell.IsEmpty)
                        {
                            return false;
                        }
                        if (ColorPath[GameVM.GameBoard[cell.X + 1][cell.Y + direction[1]].SimpleCell.Color].Equals("red-piece") ||
                            ColorPath[GameVM.GameBoard[cell.X + 1][cell.Y + direction[1]].SimpleCell.Color].Equals("red-king"))
                        {
                            return true;
                        }
                    }
                    return false;
                case "red-king-highlighted":
                case "red-king":
                    if (cell.X + direction[0] < 1 && cell.X + direction[0] > 6)
                    {
                        return false;
                    }
                    if (cell.Y + direction[1] >= 1 && cell.Y + direction[1] <= 6)
                    {
                        if (GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.IsEmpty)
                        {
                            return false;
                        }
                        if (ColorPath[GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.Color].Equals("white-piece") ||
                            ColorPath[GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.Color].Equals("white-king"))
                        {
                            return true;
                        }
                    }
                    return false;
                case "white-king-highlighted":
                case "white-king":
                    if (cell.X + direction[0] < 1 && cell.X + direction[0] > 6)
                    {
                        return false;
                    }
                    if (cell.Y + direction[1] >= 1 && cell.Y + direction[1] <= 6)
                    {
                        if (GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.IsEmpty)
                        {
                            return false;
                        }
                        if (ColorPath[GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.Color].Equals("red-piece") ||
                            ColorPath[GameVM.GameBoard[cell.X + direction[0]][cell.Y + direction[1]].SimpleCell.Color].Equals("red-king"))
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }

        private static bool CheckForLegalCaptureInDirection(Cell cell, int[] direction)
        {
            if (cell.Y + direction[1] < 1 || cell.Y + direction[1] > 6)
            {
                return false;
            }
            switch (ColorPath[cell.Color])
            {
                case "red-piece":
                    if (cell.X < 2)
                    {
                        return false;
                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X - 2][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }

                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X - 2][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }
                    }
                    return false;

                case "white-piece":
                    if (cell.X > 5)
                    {
                        return false;
                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X + 2][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }
                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X + 2][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }
                    }
                    return false;

                case "red-king":
                case "white-king":
                    if (cell.X < 2 || cell.X > 5)
                    {
                        return false;
                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X + 2 * direction[0]][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }

                    }
                    if (CheckForNeighbour(cell, direction))
                    {
                        if (GameVM.GameBoard[cell.X + 2 * direction[0]][cell.Y + 2 * direction[1]].SimpleCell.IsEmpty)
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }

        private static bool CheckForLegalCapture(Cell cell)
        {
            if (cell.IsEmpty)
            {
                return false;
            }
            switch (ColorPath[cell.Color])
            {
                case "red-piece":
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 0, -1 }))
                    {
                        return true;
                    }
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 0, 1 }))
                    {
                        return true;
                    }
                    return false;

                case "white-piece":
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 0, -1 }))
                    {
                        return true;
                    }
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 0, 1 }))
                    {
                        return true;
                    }
                    return false;
                case "red-king":
                case "white-king":
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { -1, -1 }))
                    {
                        return true;
                    }
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { -1, 1 }))
                    {
                        return true;
                    }
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 1, -1 }))
                    {
                        return true;
                    }
                    if (CheckForLegalCaptureInDirection(cell, new int[2] { 1, 1 }))
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        private static void CapturePiece(Cell cell)
        {
            cell.Color = ColorPath.FirstOrDefault(c => c.Value == null).Key;
            cell.IsEmpty = true;
        }

        private static bool IsMultipleJumpsEnabled()
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

            return settings.MultipleJumpsEnabled;
        }

        public static void HighlightPiece(Cell cell)
        {
            if (cell == null)
            {
                return;
            }
            if (cell.IsEmpty)
            {
                return;
            }
            switch (ColorPath[cell.Color])
            {
                case "red-piece":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "red-piece-highlighted").Key;
                    return;
                case "white-piece":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "white-piece-highlighted").Key;
                    return;
                case "red-king":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "red-king-highlighted").Key;
                    return;
                case "white-king":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "white-king-highlighted").Key;
                    return;
                default:
                    return;
            }
        }

        public static void UnhighlightPiece(Cell cell)
        {
            if (cell.IsEmpty)
            {
                return;
            }
            switch (ColorPath[cell.Color])
            {
                case "red-piece-highlighted":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "red-piece").Key;
                    return;
                case "white-piece-highlighted":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "white-piece").Key;
                    return;
                case "red-king-highlighted":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "red-king").Key;
                    return;
                case "white-king-highlighted":
                    cell.Color = ColorPath.FirstOrDefault(c => c.Value == "white-king").Key;
                    return;
                default:
                    return;
            }
        }
    }
}
