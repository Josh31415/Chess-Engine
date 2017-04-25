using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessEngine
{
    class Piece
    {
        private bool color;
        private string id;
        private int pieceValue;
        private Image pieceIm;

        public Piece(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
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

    }
}
