using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    class Rook : Piece
    {
        public Rook(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("rook");
            Value = 5;
            Moved = false;
        }

        public Rook()
        {

        }

        override
        public bool IsValidMove(Point p)
        {
            double slope = 0;
            double dx = p.X - this.Location.X;
            double dy = p.Y - this.Location.Y;

            if (dx != 0) slope = dy / dx;
            else return true;

            if (slope == 0) return true;
            else return false;
        }

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();

            //Find all attacked squares right of the rook
            squares.AddRange(checkVHSquares(p, (int)this.Location.X, 8));
            //Find all attacked squares left of the rook
            squares.AddRange(checkVHSquares(p, 0, (int)this.Location.X -1));
            //Find all attacked squares above the rook
            squares.AddRange(checkVHSquares(p, 0, (int)this.Location.Y - 1));
            //Find all attacked squares below the rook
            squares.AddRange(checkVHSquares(p, (int)this.Location.Y, 8));
            
            return squares;
        }
    }
}
