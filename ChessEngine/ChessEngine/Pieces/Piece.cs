using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessEngine
{
    abstract class Piece : ICloneable
    {
        private bool color;
        private bool captured;
        private bool moved;
        private string id;
        private int pieceValue;
        private Point point;
        private Image pieceIm;

        public Piece(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            Location = new Point(0, 0);
        }

        public Piece()
        {

        }

        public bool Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Value
        {
            get { return pieceValue; }
            set { pieceValue = value; }
        }

        public Image PieceImage
        {
            get { return pieceIm; }
            set { pieceIm = value; }
        }

        public Point Location
        {
            get { return point; }
            set { point = value; }
        }

        public bool Moved
        {
            get { return moved; }
            set { moved = value; }
        }

        public bool Captured
        {
            get { return captured; }
            set { captured = value; }
        }

        public abstract bool IsValidMove(Point p);
        public abstract List<Point> AttackedSquares(Piece[] pieces);
        
        // Checks if the square is occupied by another piece
        public bool checkBlock(Piece[] p, Point loc)
        {
            for (int j = 0; j < p.Length; j++)
            {
                if (p[j].Location == loc) return true;
            }

            return false;
        }

        // Finds all attacked squares in the vertical and horizontal directions
        public List<Point> checkVHSquares(Piece[] p, int start, int end)
        {
            List<Point> squares = new List<Point>();
            for (int i = start; i < end; i++)
            {
                Point loc = new Point(i, this.Location.Y);
                bool blocked = checkBlock(p, loc);
                if (!blocked) squares.Add(loc);
            }

            return squares;
        }

        // Finds all attacked squares in the positive diagonal direction 
        public List<Point> checkDiagonalSquares(Piece[] p, int start, int end)
        {
            List<Point> squares = new List<Point>();
            for (int i = start; i < end; i++)
            {
                Point loc = new Point(i, i);
                bool blocked = checkBlock(p, loc);
                if (!blocked) squares.Add(loc);
            }

            return squares;
        }

        // Converts pixel coordinates to 8x8 board grid
        public static Point boardToPiece(Point p)
        {
            Point finPoint;
            int x = (int)(p.X / 63);
            int y = (int)(p.Y / 63);

            finPoint = new Point(x, y);

            return finPoint;
        }

        public Image getImageName(string type)
        {
            string pieceColor;

            if (Color) pieceColor = "white";
            else pieceColor = "black";

            string im = "PieceImages/" + type + "-" + pieceColor + ".png";
            Image tempIm = createPiece(im);
            tempIm.AllowDrop = true;

            return tempIm;
        }

        private Image createPiece(string name)
        {
            Image im = new Image();
            im.Height = 60;
            im.Width = 60;
            im.Source = new BitmapImage(new Uri(name, UriKind.RelativeOrAbsolute));

            return im;
        }

        public bool movePiece(Point p)
        {
            if (p.X > 7) p.X = 7;
            else if (p.X < 0) p.X = 0;
            else if (p.Y > 7) p.Y = 7;
            else if (p.Y < 0) p.Y = 0;

            if (IsValidMove(p)) return true;
            else return false;
        }

        public Point locationOnBoard()
        {
            Point p = new Point();

            p.X = this.Location.X * 63;
            p.Y = this.Location.Y * 63;

            return p;
        }

        public bool Equals(Piece p)
        {
            if(p.Color != Color) return false;
            if (p.Moved != Moved) return false;
            if (p.Id != Id) return false;
            if (p.pieceValue != pieceValue) return false;
            if (p.point != point) return false;
            return true;
        }

        public object Clone()
        {
            Piece newPiece;
            switch(this.GetType().FullName)
            {
                case "King":
                    newPiece = new King(this.Color, this.Id);
                    break;
                case "Queen":
                    newPiece = new Queen(this.Color, this.Id);
                    break;
                case "Rook":
                    newPiece = new Rook(this.Color, this.Id);
                    break;
                case "Bishop":
                    newPiece = new Bishop(this.Color, this.Id);
                    break;
                case "Knight":
                    newPiece = new Knight(this.Color, this.Id);
                    break;
                case "Pawn":
                    newPiece = new Pawn(this.Color, this.Id);
                    break;
                default:
                    return null;
            }

            newPiece.Location = this.Location;
            newPiece.Moved = this.Moved;
            newPiece.Captured = this.Captured;

            return newPiece;
        }
    }
}
