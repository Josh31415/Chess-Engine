using System.Windows;

namespace ChessEngine
{
    class Board
    {
        private int size;
        private int squareSize;

        public Board(int size)
        {
            Size = size;
            SquareSize = (size / 8) + (size / 300);
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int SquareSize
        {
            get { return squareSize; }
            set { squareSize = value; }
        }

        public int findPiece(Point p, Piece[] piece)
        {
            for (int i = 0; i < 32; i++)
            {
                int xError = (int)(p.X - piece[i].locationOnBoard().X);
                int yError = (int)(p.Y - piece[i].locationOnBoard().Y);

                if (xError < squareSize && yError < squareSize)
                {
                    if (xError > 0 && yError > 0)
                    {
                        if(!piece[i].Captured) return i;
                    }
                }
            }

            return -1;
        }

        public Point toBoardCorrdinates(Point location)
        {
            Point p = new Point();

            p.X = location.X * squareSize;
            p.Y = location.Y * squareSize;

            return p;
        }
    }
}
