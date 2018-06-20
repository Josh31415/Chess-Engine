namespace ChessEngine
{
    class HvsHGame : Game, IGame
    {
        private Piece[] piece = new Piece[32];
        private Check check;
        GameFile file;

        public HvsHGame()
        {
            SetupBoard();
            check.isCheck = false;
            file = new GameFile("../../pgns/temp.pgn");
        }
    }
}
