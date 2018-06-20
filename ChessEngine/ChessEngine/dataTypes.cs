using System.Windows;

namespace ChessEngine
{
    struct Check
    {
        public bool isCheck;
        public bool checkColor;
    }

    struct PieceMove
    {
        public Piece piece;
        public bool check;
        public bool capture;
        public Point newLocation;
    }

    struct BoardEvaluation
    {
        public float gameValue;
        public int pieceValueDiff;
        public float[][] squareValues;
    }

    struct ConditionalMove
    {
        public int moveNum;
        public PieceMove moveOrder;
    }
}