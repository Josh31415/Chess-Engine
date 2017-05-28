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
        public static bool checkPiecePath(Point oldLoc, Point newLoc, Piece[] piece)
        {
            double slope = 0;
            double dy = newLoc.Y - oldLoc.Y;
            double dx = newLoc.X - oldLoc.X;
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
                        loc.X = oldLoc.X + i;
                        loc.Y = oldLoc.Y + i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }
            if (slope == -1)
            {
                Console.WriteLine("slope is -1");
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
                        loc.X = oldLoc.X - i;
                        loc.Y = oldLoc.Y + i;

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
                        loc.X = oldLoc.X + i;
                        loc.Y = oldLoc.Y;

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
                        loc.X = oldLoc.X - i;
                        loc.Y = oldLoc.Y;

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
                        loc.X = oldLoc.X;
                        loc.Y = oldLoc.Y + i;

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
                        loc.X = oldLoc.X;
                        loc.Y = oldLoc.Y - i;

                        for (int j = 0; j < piece.Length; j++)
                        {
                            if (piece[j].Location == loc) return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
