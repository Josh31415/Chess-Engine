using System.Windows;

namespace ChessEngine
{
    struct Check
    {
        public bool isCheck;
        public bool checkColor;
    }

    struct pieceMove
    {
        public Piece piece;
        public bool check;
        public bool capture;
        public Point newLocation;
    }
}