using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Controls;
using System;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ChessEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Piece
    {

        private string type;
        private string color;
        private string id;
        private Image pieceIm;
        

        public Piece(string piecetype, string piececolor, string pieceid)
        {
            type = piecetype;
            color = piececolor;
            pieceIm = pieceImage();
            id = pieceid;
        }

        private Image pieceImage()
        {
            string im = type + "-" + color;
            Image tempIm = MainWindow.CreatePiece(im, 0);
            tempIm.AllowDrop = true;
            return tempIm;
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public Image PieceImage
        {
            get { return pieceIm; }
            set { pieceIm = value; }
        }
        
    }

    public partial class MainWindow : Window
    {
        Image[] pieces = new Image[32];

        ObservableCollection<Piece> piece = new ObservableCollection<Piece>();

        Point pos;
        private Control activeControl;
        private Point previousLocation;

        int gridCol = 0;
        int gridRow = 0;

        //White = 0, Black = 1
        public static Image CreatePiece(string name, int color)
        {
            string pieceName = name + ".png";

            Image im = new Image();
            im.Height = 60;
            im.Width = 60;
            im.Source = new BitmapImage(new Uri(pieceName, UriKind.RelativeOrAbsolute));

            return im;
        }

        public void SetupBoard()
        {
            piece.Add(new Piece("rook", "black", "rbk"));
            piece.Add(new Piece("knight", "black", "nbk"));
            piece.Add(new Piece("bishop", "black", "bbk"));
            piece.Add(new Piece("king", "black", "kb"));
            piece.Add(new Piece("queen", "black", "qb"));
            piece.Add(new Piece("bishop", "black", "bbq"));
            piece.Add(new Piece("knight", "black", "nbq"));
            piece.Add(new Piece("rook", "black", "rbq"));

            piece.Add(new Piece("pawn", "black", "pba"));
            piece.Add(new Piece("pawn", "black", "pbb"));
            piece.Add(new Piece("pawn", "black", "pbc"));
            piece.Add(new Piece("pawn", "black", "pbd"));
            piece.Add(new Piece("pawn", "black", "pbe"));
            piece.Add(new Piece("pawn", "black", "pbf"));
            piece.Add(new Piece("pawn", "black", "pbg"));
            piece.Add(new Piece("pawn", "black", "pbh"));

            piece.Add(new Piece("rook", "white", "rwk"));
            piece.Add(new Piece("knight", "white", "nwk"));
            piece.Add(new Piece("bishop", "white", "bwk"));
            piece.Add(new Piece("king", "white", "kw"));
            piece.Add(new Piece("queen", "white", "qw"));
            piece.Add(new Piece("bishop", "white", "bbw"));
            piece.Add(new Piece("knight", "white", "nbw"));
            piece.Add(new Piece("rook", "white", "rbw"));

            piece.Add(new Piece("pawn", "white", "pba"));
            piece.Add(new Piece("pawn", "white", "pbb"));
            piece.Add(new Piece("pawn", "white", "pbc"));
            piece.Add(new Piece("pawn", "white", "pbd"));
            piece.Add(new Piece("pawn", "white", "pbe"));
            piece.Add(new Piece("pawn", "white", "pbf"));
            piece.Add(new Piece("pawn", "white", "pbg"));
            piece.Add(new Piece("pawn", "white", "pbh"));

            for(int i = 0; i < 32; i++)
            {
                ChessBoard.Children.Add(piece[i].PieceImage);
            }
            
            for(int i = 0; i < 8; i++)
            {
                Grid.SetColumn(piece[i].PieceImage, i);
            }
            for (int i = 0; i < 8; i++)
            {
                Grid.SetRow(piece[i].PieceImage, 0);
            }

            for (int i = 8; i < 16; i++) Grid.SetColumn(piece[i].PieceImage, (i - 8));
            for (int i = 8; i < 16; i++) Grid.SetRow(piece[i].PieceImage, 1);
            for (int i = 24; i < 32; i++) Grid.SetColumn(piece[i].PieceImage, (i - 24));
            for (int i = 24; i < 32; i++) Grid.SetRow(piece[i].PieceImage, 6);
            for (int i = 16; i < 24; i++) Grid.SetColumn(piece[i].PieceImage, (i - 16));
            for (int i = 16; i < 24; i++) Grid.SetRow(piece[i].PieceImage, 7);
           
        }



        public MainWindow()
        {
            InitializeComponent();

            SetupBoard();

            ChessBoard.AllowDrop = true;
            piece[0].PieceImage.AllowDrop = true;

            //piece[0].PieceImage.DragEnter += piece_DragEnter;
            piece[0].PieceImage.MouseLeftButtonDown += new MouseButtonEventHandler(piece_MouseDown);
            // piece[0].PieceImage.MouseMove += new MouseEventHandler(piece_MouseMove);
            piece[0].PieceImage.DragLeave += new DragEventHandler(piece_MouseMove);

            piece[0].PieceImage.MouseLeftButtonUp += new MouseButtonEventHandler(piece_MouseLeave);
            
        }

        void piece_MouseDown(object sender, MouseEventArgs e)
        {


            activeControl = sender as Control;
            previousLocation = e.GetPosition(activeControl);
            
            ChessBoard.Children.Remove(piece[0].PieceImage);
            ChessBoard.Children.Add(piece[0].PieceImage);

            Cursor = Cursors.Hand;
            // Image image = e.Source as Image;
            /// DataObject data = new DataObject(typeof(ImageSource), image.Source);

            // DragDrop.DoDragDrop(image, data, DragDropEffects.Move);

        }

        void piece_MouseMove(object sender, DragEventArgs e)
        {
            var element = (UIElement)e.Source;
            
            Point currPosx = Mouse.GetPosition(ChessBoard);
            
            piece[0].PieceImage.TranslatePoint(currPosx, ChessBoard);
            
            gridCol = Grid.GetColumn(element);
            gridRow = Grid.GetRow(element);
            piece[0].PieceImage.RenderTransform.Transform(currPosx);
            //location.Offset(currPosx.X - previousLocation.X, currPosx.Y - previousLocation.Y);
            //this.activeControl.Location = location;
        }
        
        private void piece_MouseLeave(object sender, MouseEventArgs e)
        {
            //ImageSource image = e.Data.GetData(typeof(ImageSource)) as ImageSource;
            
            //Image imageControl = new Image() { Width = 60, Height = 60, Source =  };
            Cursor = Cursors.Arrow;
            ChessBoard.Children.Remove(piece[0].PieceImage);
            ChessBoard.Children.Add(piece[0].PieceImage);
            Grid.SetColumn(piece[0].PieceImage, gridCol);
            Grid.SetRow(piece[0].PieceImage, gridRow);

        }
        
        

        public int[] GetPiecePosition(UIElement element)
        {
            var point = Mouse.GetPosition(element);
            int row = 0;
            int col = 0;
            int[] location = new int[2];
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calculate the row mouse was over
            foreach (var rowDefinition in ChessBoard.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calcutate column mouse was over
            foreach (var columnDefinition in ChessBoard.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }
            location[0] = row;
            location[1] = col;
            return location;
        }

        
    }
}
