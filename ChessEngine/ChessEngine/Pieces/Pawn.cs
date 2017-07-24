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

        override
        public List<Point> AttackedSquares(Piece[] p)
        {
            List<Point> squares = new List<Point>();
            if (Color)
            {
                if (Location.X - 1 >= 0 && Location.Y - 1 >= 0) squares.Add(new Point(Location.X - 1, Location.Y - 1));
                if (Location.X + 1 < 8 && Location.Y - 1 >= 0) squares.Add(new Point(Location.X + 1, Location.Y - 1));
            }
            else
            {
                if (Location.X - 1 >= 0 && Location.Y + 1 < 8) squares.Add(new Point(Location.X - 1, Location.Y + 1));
                if (Location.X + 1 >= 0 && Location.Y + 1 < 8) squares.Add(new Point(Location.X + 1, Location.Y + 1));
            }

            return squares;
        }

    }
}
