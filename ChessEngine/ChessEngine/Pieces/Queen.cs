using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    } 
}
