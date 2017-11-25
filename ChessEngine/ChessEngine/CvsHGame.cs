using System;
using System.Collections.Generic;
using System.Windows;

namespace ChessEngine
{
    class CvsHGame : Game, IGame
    {

        public CvsHGame()
        {
            SetupBoard();
            setCheck(false, false);
            gameFile = new GameFile("../../pgns/temp.pgn");
        }

        // Gets the sum of the players uncaptured piece values
        public (int, int) getPlayerValues()
        {
            int bVal = 0, wVal = 0;

            for(int i = 0; i < Pieces.Length; i++)
            {
                if(!Pieces[i].Captured)
                {
                    if (Pieces[i].Color) wVal += Pieces[i].Value;
                    else bVal += Pieces[i].Value;
                }
            }

            return (wVal, bVal);
        }

        public float getAttackValue(List<Point> attackedSq, Piece p)
        {
            float value = 0;

            for(int i = 0; i < Pieces.Length; i++)
            {
                // Checks if the piece is attacking a square occupied by an opponent's piece
                if(attackedSq.Contains(Pieces[i].Location) && p.Color != Pieces[i].Color)
                {
                    int defValue = 0;

                    // Checks if the attacked piece is defended
                    for (int j = 0; j < Pieces.Length; j++)
                    {
                        List<Point> atSq = Pieces[i].AttackedSquares(Pieces);
                        if(atSq.Contains(Pieces[i].Location) && Pieces[i].Color == Pieces[j].Color)
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

        public float getBoardEvaluation()
        {
            (int whiteValue, int blackValue) = getPlayerValues();
            float totalValue = whiteValue - blackValue;

            for (int i = 0; i < Pieces.Length; i++)
            {
                List<Point> atSq = Pieces[i].AttackedSquares(Pieces);
                float attackValue = getAttackValue(atSq, Pieces[i]);
                attackValue += (atSq.ToArray().Length * (float)0.1);
                if (Pieces[i].Color) totalValue += attackValue;
                else totalValue -= attackValue;
            }

            if(getCheck().isCheck)
            {
                if (getCheck().checkColor) totalValue += totalValue * (float) 0.5;
                else totalValue -= totalValue * (float) 0.5;
            }

            return totalValue;
        }

        public bool NextTurn()
        {
            float eval = getBoardEvaluation();

            return true;
        }
    }
}
