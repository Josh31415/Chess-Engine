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
            turn = true;
            setCheck(false, false);
            gameFile = new GameFile("../../pgns/temp.pgn");
        }

        public void buildMoveTree(int depth, int width)
        {
            float eval = BoardEval();

            PieceMove[] moves = GameEvaluation.getMoveList(width, Pieces);
        }

        public new bool CheckMove(Point position, int index)
        {
            if (base.CheckMove(position, index))
            {
                buildMoveTree(3, 5);
                return true;
            }
            else
            {
                return false;
            }
        }

        public float BoardEval()
        {
            Console.WriteLine("next turn");
            float eval = GameEvaluation.getBoardEvaluation(Pieces, this.getCheck());
            Console.WriteLine("Eval completed");

            return eval;
        }
    }
}
