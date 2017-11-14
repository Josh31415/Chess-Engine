using System;
using System.Windows;

namespace ChessEngine
{
    class CvsHGame : Game, IGame
    {

        public CvsHGame()
        {
            SetupBoard();
            check = new Check();
            check.isCheck = false;
            gameFile = new GameFile("../../pgns/temp.pgn");
        }

        public bool CheckMove(Point position, int index)
        {
            throw new NotImplementedException();
        }

        public bool NextTurn()
        {
            turn = !turn;
            return true;
        }
    }
}
