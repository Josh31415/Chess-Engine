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
        }

        override
        public bool IsValidMove(Point p)
        {
            int slope = 0;
            int dx = (int)p.X - (int)this.Location.X;
            int dy = (int)p.Y - (int)this.Location.Y;

            if (dx != 0) slope = dy / dx;
            else return true;

            if (slope == 0) return true;
            else return false;
        }

    }
}
