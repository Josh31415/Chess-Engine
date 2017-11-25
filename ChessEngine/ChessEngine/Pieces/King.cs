using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    class King : Piece
    {
        public King(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("king");
            Moved = false;
        }

        override
        public bool IsValidMove(Point p)
        {
            if(Math.Abs(p.X - this.Location.X) > 1 || Math.Abs(p.Y - this.Location.Y) > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();

            squares.Add(new Point(Location.X, Location.Y - 1));
            squares.Add(new Point(Location.X - 1, Location.Y));
            squares.Add(new Point(Location.X, Location.Y + 1));
            squares.Add(new Point(Location.X + 1, Location.Y));
            squares.Add(new Point(Location.X + 1, Location.Y + 1));
            squares.Add(new Point(Location.X + 1, Location.Y - 1));
            squares.Add(new Point(Location.X - 1, Location.Y + 1));
            squares.Add(new Point(Location.X + 1, Location.Y - 1));

            for (int i = 0; i < squares.Count; i++)
            {
                Point pt = squares.ElementAt(i);
                if (checkBlock(p, boardToPiece(pt))) squares.Remove(pt);
            }

            return squares;
        }
    }
}
