using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    class Bishop : Piece
    {
        public Bishop(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("bishop");
            Value = 3;
            Moved = false;
        }

        override
        public bool IsValidMove(Point p)
        {
            double slope = 0;
            double dx = p.X - this.Location.X;
            double dy = p.Y - this.Location.Y;

            if (dx != 0) slope = dy / dx;

            if (slope == 1 || slope == -1) return true;
            else if (dx == 0 && dy == 0) return false;
            else return false;
        }

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();

            //Find all attacked squares diagonal to the upper right the queen
            squares.AddRange(checkDiagonalSquares(p, 0, (int)this.Location.X - 1));
            //Find all attacked squares diagonal to the upper left the queen
            squares.AddRange(checkDiagonalSquares(p, 0, (int)this.Location.X - 1));
            //Find all attacked squares diagonal to the lower right the queen
            squares.AddRange(checkDiagonalSquares(p, 0, (int)this.Location.Y - 1));
            //Find all attacked squares diagonal to the lower left the queen
            squares.AddRange(checkDiagonalSquares(p, (int)this.Location.Y, 8));

            return squares;
        }
    }
}
