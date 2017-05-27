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
    }
}
