using System.Collections.Generic;
using System.Windows;

namespace ChessEngine
{
    class GameEvaluation
    {

        // Gets the sum of the players uncaptured piece values
        public static (int, int) getPlayerValues(Piece[] Pieces)
        {
            int bVal = 0, wVal = 0;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (!Pieces[i].Captured)
                {
                    if (Pieces[i].Color) wVal += Pieces[i].Value;
                    else bVal += Pieces[i].Value;
                }
            }

            return (wVal, bVal);
        }

        private static float getAttackValue(Piece[] Pieces, List<Point> attackedSq, Piece p)
        {
            float value = 0;

            for (int i = 0; i < Pieces.Length; i++)
            {
                // Checks if the piece is attacking a square occupied by an opponent's piece
                if (attackedSq.Contains(Pieces[i].Location) && p.Color != Pieces[i].Color)
                {
                    int defValue = 0;

                    // Checks if the attacked piece is defended
                    for (int j = 0; j < Pieces.Length; j++)
                    {
                        List<Point> atSq = Pieces[i].AttackedSquares(Pieces);
                        if (atSq.Contains(Pieces[i].Location) && Pieces[i].Color == Pieces[j].Color)
                        {
                            defValue += Pieces[j].Value;
                        }
                    }

                    if (defValue == 0) value += 3;
                    else
                    {
                        if (defValue > p.Value) value += p.Value / defValue;
                        else value += p.Value - defValue;
                    }
                }
            }

            return value;
        }

        public static float getBoardEvaluation(Piece[] Pieces,  Check check)
        {
            (int whiteValue, int blackValue) = getPlayerValues(Pieces);
            float totalValue = whiteValue - blackValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                List<Point> atSq = Pieces[i].AttackedSquares(Pieces);
                float attackValue = getAttackValue(Pieces, atSq, Pieces[i]);
                attackValue += (atSq.ToArray().Length * (float)0.1);
                if (Pieces[i].Color) totalValue += attackValue;
                else totalValue -= attackValue;
            }

            if (check.isCheck)
            {
                if (check.checkColor) totalValue += totalValue * (float)0.5;
                else totalValue -= totalValue * (float)0.5;
            }

            return totalValue;
        }

        // gets integer values for each square on the board
        public static int[][] getSquareValues(Piece[] Pieces)
        {
            int[][] squareVal = new int[8][];

            for (int i = 0; i < Pieces.Length; i++)
            {
                Point[] atSq = Pieces[i].AttackedSquares(Pieces).ToArray();

                for (int j = 0; j < atSq.Length; j++)
                {
                    int xLoc = (int)atSq[j].X - 1;
                    int yLoc = (int)atSq[j].Y - 1;

                    // add or subtract piece value from attacked square
                    if (Pieces[i].Color) squareVal[xLoc][yLoc] += Pieces[i].Value;
                    else squareVal[xLoc][yLoc] -= Pieces[i].Value;
                }
            }

            return squareVal;
        }
    }
}
