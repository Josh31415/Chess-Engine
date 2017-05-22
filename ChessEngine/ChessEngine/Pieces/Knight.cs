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

        }

        override
        public bool IsValidMove(Point p)
        {
            return true;
        }

    }
}
