using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessEngine
{
    abstract class Piece
    {
        private bool color;
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

        public abstract bool IsValidMove(Point p);

        public Image getImageName(string type)
        {
            string pieceColor;

            if (Color) pieceColor = "white";
            else pieceColor = "black";

            string im = type + "-" + pieceColor + ".png";
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

        public bool movePiece(Point location)
        {
            Point p;
            int x =(int) location.X / 63;
            int y = (int)location.Y / 63;

            if (x > 7) x = 7;
            else if (x < 0) x = 0;
            else if (y > 7) x = 7;
            else if (y < 0) x = 0;

            p = new Point(x, y);

            if (IsValidMove(p)){
                this.Location = p;
                return true;
            }
            else
            {
                return false;
            }


        }
       
        public Point locationOnBoard(Point location)
        {
            Point p = new Point();

            p.X = location.X * 63;
            p.Y = location.Y * 63;

            return p;
        }

        public Point locationOnBoard()
        {
            Point p = new Point();

            p.X = this.Location.X * 63;
            p.Y = this.Location.Y * 63;

            return p;
        }

    }
}
