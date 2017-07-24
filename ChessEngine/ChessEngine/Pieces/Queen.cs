using System.Collections.Generic;
using System.Windows;

namespace ChessEngine
{
    class Queen : Piece
    {
        public Queen(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("queen");
            Value = 9;
            Moved = false;
        }

        override
        public bool IsValidMove(Point p)
        {
            double slope = 0;
            double dx = p.X - this.Location.X;
            double dy = p.Y - this.Location.Y;

            if (dx != 0) slope = dy / dx;
            else if (dx == 0 && dy == 0) return false;

            if (slope == 0 || slope == 1 || slope == -1) return true;
            else return false;
        }

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();

            //Find all attacked squares right of the queen
            squares.AddRange(checkVHSquares(p, (int)this.Location.X, 8));
            //Find all attacked squares left of the queen
            squares.AddRange(checkVHSquares(p, 0, (int)this.Location.X - 1));
            //Find all attacked squares above the queen
            squares.AddRange(checkVHSquares(p, 0, (int)this.Location.Y - 1));
            //Find all attacked squares below the queen
            squares.AddRange(checkVHSquares(p, (int)this.Location.Y, 8));
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
