using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ChessEngine
{
    class Pawn : Piece
    {
       
        public Pawn(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("pawn");
            Value = 1;
            Moved = false;
        }

        
        override
        public bool IsValidMove(Point p)
        {
            int xloc = (int)(this.Location.X - p.X);

            if (this.Color)
            {
                int loc = (int)(this.Location.Y - p.Y);
               
                if (loc > 2 || loc <= 0) return false;
                else if (xloc != 0) return false;
                else if (loc == 2 && Moved) return false;
                else return true;
               
            }
            else
            {
                int loc = (int)(p.Y - this.Location.Y);

                if (loc > 2 || loc <= 0) return false;
                else if (xloc != 0)return false;
                else if (loc == 2 && Moved) return false;
                else return true;
            }
        }

    }
}
