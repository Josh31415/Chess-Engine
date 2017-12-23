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

        public void buildMoveTree(int depth, int width)
        {
            
        }

        public bool NextTurn()
        {
            float eval = GameEvaluation.getBoardEvaluation(Pieces, this.getCheck());


            return true;
        }
    }
}
