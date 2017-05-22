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
        }

        override
        public bool IsValidMove(Point p)
        {
            int slope = 0;
            int dx = (int) p.X - (int) this.Location.X;
            int dy = (int) p.Y - (int) this.Location.Y;

            if(dx != 0) slope = dy / dx;

            if (slope == 0 || slope == 1 || slope == -1) return true;
            else if (dx == 0 && dy == 0) return false;
            else return false;
        }
    }
}
