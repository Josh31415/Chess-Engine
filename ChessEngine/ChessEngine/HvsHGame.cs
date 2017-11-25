using System.Windows;

namespace ChessEngine
{
    class HvsHGame : Game, IGame
    {
        private Piece[] piece = new Piece[32];
        private bool turn;
        private Check check;
        GameFile file;

        public HvsHGame()
        {
            SetupBoard();
            check.isCheck = false;
            file = new GameFile("../../pgns/temp.pgn");
        }

        public bool NextTurn()
        {
            turn = !turn;
            return true;
        }
    }
}
