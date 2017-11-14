using System.Windows;

namespace ChessEngine
{
    abstract class Game
    {
        private Piece[] piece = new Piece[32];
        private bool turn;
        private Check chck;
        GameFile file;

        public Piece[] getPieces()
        {
            return piece;
        }

        public Piece[] Pieces
        {
            get { return piece; }
            set { piece = value; }
        }

        public Check check
        {
            get { return chck; }
            set { chck = value; }
        }

        public GameFile gameFile
        {
            get { return file; }
            set { file = value; }
        }

        public void SetupBoard()
        {
            piece[0] = new Rook(false, "rbk");
            piece[1] = new Knight(false, "nbk");
            piece[2] = new Bishop(false, "bbk");
            piece[3] = new Queen(false, "qb");
            piece[4] = new King(false, "kb");
            piece[5] = new Bishop(false, "bbq");
            piece[6] = new Knight(false, "nbq");
            piece[7] = new Rook(false, "rbq");

            piece[8] = new Pawn(false, "pba");
            piece[9] = new Pawn(false, "pbb");
            piece[10] = new Pawn(false, "pbc");
            piece[11] = new Pawn(false, "pbd");
            piece[12] = new Pawn(false, "pbe");
            piece[13] = new Pawn(false, "pbf");
            piece[14] = new Pawn(false, "pbg");
            piece[15] = new Pawn(false, "pbh");

            piece[16] = new Rook(true, "rwk");
            piece[17] = new Knight(true, "nwk");
            piece[18] = new Bishop(true, "bwk");
            piece[19] = new Queen(true, "qw");
            piece[20] = new King(true, "kw");
            piece[21] = new Bishop(true, "bbw");
            piece[22] = new Knight(true, "nbw");
            piece[23] = new Rook(true, "rbw");

            piece[24] = new Pawn(true, "pba");
            piece[25] = new Pawn(true, "pbb");
            piece[26] = new Pawn(true, "pbc");
            piece[27] = new Pawn(true, "pbd");
            piece[28] = new Pawn(true, "pbe");
            piece[29] = new Pawn(true, "pbf");
            piece[30] = new Pawn(true, "pbg");
            piece[31] = new Pawn(true, "pbh");

            // set the first turn to white
            turn = true;

            // Adds the black home row to the board
            for (int i = 0; i < 8; i++) piece[i].Location = new Point(i, 0);

            // Adds the black pawns to the board
            for (int i = 8; i < 16; i++) piece[i].Location = new Point(i - 8, 1);

            // Adds the white home row to the board
            for (int i = 16; i < 24; i++) piece[i].Location = new Point(i - 16, 7);

            // Adds the White pawns to the board
            for (int i = 24; i < 32; i++) piece[i].Location = new Point(i - 24, 6);
        }
    }
}