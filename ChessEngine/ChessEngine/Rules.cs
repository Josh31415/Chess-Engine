using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    static class Rules
    {
        public static bool checkPiecePath(Piece old, Point newLoc, Piece[] piece)
        {
            double slope = 0;
            double dy = newLoc.Y - old.Location.Y;
            double dx = newLoc.X - old.Location.X;
            Point loc = new Point();

            if (dx != 0) slope = dy / dx;
            else slope = double.NaN;

            if (slope == 1)
            {
                if (dx < 0)
                {
                    for (int i = 1; i < Math.Abs(dx); i++)
                    {
                        loc.X = newLoc.X + i;
                        loc.Y = newLoc.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
                else if (dx > 0)
                {
                    for (int i = 1; i < Math.Abs(dx); i++)
                    {
                        loc.X = old.Location.X + i;
                        loc.Y = old.Location.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }
            if (slope == -1)
            {
                if (dx > 0)
                {
                    for (int i = 1; i < dx; i++)
                    {
                        loc.X = newLoc.X - i;
                        loc.Y = newLoc.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
                else if (dx < 0)
                {
                    for (int i = 1; i < Math.Abs(dx); i++)
                    {
                        loc.X = old.Location.X - i;
                        loc.Y = old.Location.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }
            else if (slope == 0)
            {
                if (dx > 0)
                {
                    for (int i = 1; i < Math.Abs(dx); i++)
                    {
                        loc.X = old.Location.X + i;
                        loc.Y = old.Location.Y;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
                else if (dx < 0)
                {
                    for (int i = 1; i < Math.Abs(dx); i++)
                    {
                        loc.X = old.Location.X - i;
                        loc.Y = old.Location.Y;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }
            else if (double.IsNaN(slope))
            {
                if (dy > 0)
                {
                    for (int i = 1; i < Math.Abs(dy); i++)
                    {
                        loc.X = old.Location.X;
                        loc.Y = old.Location.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
                else if (dy < 0)
                {
                    for (int i = 1; i < Math.Abs(dy); i++)
                    {
                        loc.X = old.Location.X;
                        loc.Y = old.Location.Y - i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }

            for (int i = 0; i < piece.Length; i++)
            {
                if(piece[i].Location == newLoc)
                {
                    if (piece[i].Color == old.Color) return false;
                }
            }

                    return true;
        }

        public static bool checkCastle(Piece king, Point newLoc, Piece[] pieces, out int location)
        {
            location = 0;
            double dy = newLoc.Y - king.Location.Y;
            double dx = newLoc.X - king.Location.X;

            if (Math.Abs(dx) != 2) return false;
            else if(dy != 0) return false;
            else if (king.Moved) return false;

            for(int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].GetType().Equals(typeof(Rook)))
                {
                    if (pieces[i].Moved)
                    {
                    }
                    else if (pieces[i].Location.X - newLoc.X == 1 && pieces[i].Location.Y == newLoc.Y)
                    {
                        location = i;
                        return true;
                    }
                    else if (pieces[i].Location.X - newLoc.X == -2 && pieces[i].Location.Y == newLoc.Y)
                    {
                        location = i;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckCapture(Point loc, Piece[] p, out int capIndex)
        {
            capIndex = 0;
            for(int i = 0; i < p.Length; i++)
            {
                if(p[i].Location == loc)
                {
                    capIndex = i;
                    return true;
                }
            }
            return false;
        }
    }
}
