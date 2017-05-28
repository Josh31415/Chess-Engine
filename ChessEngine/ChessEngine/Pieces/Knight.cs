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
            Moved = false;
        }

        override
        public bool IsValidMove(Point p)
        {
            float slope = 0.0F;
            float dx = (float) (p.X - this.Location.X);
            float dy = (float)(p.Y - this.Location.Y);
            Console.WriteLine(slope);
            Console.WriteLine(dy);
            Console.WriteLine(dx);
            slope = dy / dx;
            Console.WriteLine(slope);
            // if (dx != 0) 
            //else if (dx == 0 && dy == 0) return false;

            if (slope == 0.5 || slope == -0.5 || slope == 2 || slope == -2)
            {
                if (dx < 3 && dy < 3) return true;
                else return false;
            }
            else
            {
                return false;
            }
            
        }

    }
}
