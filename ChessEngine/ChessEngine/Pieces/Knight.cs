using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    class Knight : Piece
    {
        public Knight(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("knight");
            Value = 3;
            Moved = false;
        }

        override
        public bool IsValidMove(Point p)
        {
            float slope = 0.0F;
            float dx = (float) (p.X - this.Location.X);
            float dy = (float)(p.Y - this.Location.Y);
            slope = dy / dx;
            
            if (slope == 0.5 || slope == -0.5 || slope == 2 || slope == -2)
            {
                if (dx < 3 && dy < 3) return true;
                else return false;
            }
            else
            {
                return false;
            }
            
        }

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();

            if (Location.X + 1 < 8 && Location.Y + 2 < 8) squares.Add(new Point(Location.X + 1, Location.Y + 2));
            if (Location.X + 1 < 8 && Location.Y - 2 >= 0) squares.Add(new Point(Location.X + 1, Location.Y - 2));
            if (Location.X - 1 >= 0 && Location.Y + 2 < 8) squares.Add(new Point(Location.X - 1, Location.Y + 2));
            if (Location.X - 1 >= 0 && Location.Y - 2 >= 0) squares.Add(new Point(Location.X - 1, Location.Y - 2));
            if (Location.X + 1 < 8 && Location.Y + 2 < 8) squares.Add(new Point(Location.X + 2, Location.Y + 1));
            if (Location.X + 1 < 8 && Location.Y - 2 >= 0) squares.Add(new Point(Location.X + 2, Location.Y - 1));
            if (Location.X - 1 >= 0 && Location.Y + 2 < 8) squares.Add(new Point(Location.X - 2, Location.Y + 1));
            if (Location.X - 1 >= 0 && Location.Y - 2 >= 8) squares.Add(new Point(Location.X - 2, Location.Y - 1));

            for (int i = 0; i < squares.Count; i++)
            {
                Point pt = squares.ElementAt(i);
                if (checkBlock(p, boardToPiece(pt))) squares.Remove(pt);
            }

            return squares;
        }

    }
}
