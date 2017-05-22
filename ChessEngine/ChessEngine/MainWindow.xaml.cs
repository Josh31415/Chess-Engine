using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Timers;

namespace ChessEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private Piece[] piece = new Piece[32];
        private int index;
        private bool turn;

        public void SetupBoard()
        {
            piece[0] = new Rook(false, "rbk");
            piece[1] = new Knight(false, "nbk");
            piece[2] = new Bishop(false, "bbk");
            piece[3] = new King(false, "kb");
            piece[4] = new Queen(false, "qb");
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
            piece[19] = new King(true, "kw");
            piece[20] = new Queen(true, "qw");
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
            for (int i = 0; i < 8; i++)
            {
                piece[i].Location = new Point(i, 0);

                Point p = piece[i].locationOnBoard();

                Canvas.SetLeft(piece[i].PieceImage, p.X);
                Canvas.SetTop(piece[i].PieceImage, p.Y);
                
                ChessBoard.Children.Add(piece[i].PieceImage);
            }

            // Adds the black pawns to the board
            for (int i = 8; i < 16; i++)
            {
                piece[i].Location = new Point(i - 8, 1);
                Point p = piece[i].locationOnBoard();

                Canvas.SetLeft(piece[i].PieceImage, p.X);
                Canvas.SetTop(piece[i].PieceImage, p.Y);

                ChessBoard.Children.Add(piece[i].PieceImage);
            }

            // Adds the white home row to the board
            for (int i = 16; i < 24; i++)
            {
                piece[i].Location = new Point(i - 16, 7);
                Point p = piece[i].locationOnBoard();

                Canvas.SetLeft(piece[i].PieceImage, p.X);
                Canvas.SetTop(piece[i].PieceImage, p.Y);

                ChessBoard.Children.Add(piece[i].PieceImage);
            }

            // Adds the White pawns to the board
            for (int i = 24; i < 32; i++)
            {
                piece[i].Location = new Point(i - 24, 6);
                Point p = piece[i].locationOnBoard();

                Canvas.SetLeft(piece[i].PieceImage, p.X);
                Canvas.SetTop(piece[i].PieceImage, p.Y);

                ChessBoard.Children.Add(piece[i].PieceImage);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            SetupBoard();
            
            for(int i = 0; i < 32; i++) piece[i].PieceImage.AllowDrop = true;
        }

        // finds the location of a piece on the board
        public int findPiece(Point p)
        {
            for(int i = 0; i < 32; i++)
            {
                int xError = (int) (p.X - piece[i].locationOnBoard().X);
                int yError = (int)(p.Y - piece[i].locationOnBoard().Y);

                if ( xError < 63 && yError < 63)
                {
                    if (xError > 0 && yError > 0)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point p = e.GetPosition(ChessBoard);
            index = findPiece(p);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
           
            if (e.LeftButton == MouseButtonState.Pressed && index != -1)
            {
                Point p = e.GetPosition(ChessBoard);

                int top = Canvas.GetZIndex(piece[index].PieceImage);
                foreach (Image child in ChessBoard.Children)
                {
                    if (top < Canvas.GetZIndex(child)) top = Canvas.GetZIndex(child);
                }

                p.X = p.X - 30;
                p.Y = p.Y - 30;
                Canvas.SetTop(piece[index].PieceImage, p.Y);
                Canvas.SetLeft(piece[index].PieceImage, p.X);
                Canvas.SetZIndex(piece[index].PieceImage, top + 1);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (index != -1)
            { 
                if(piece[index].Color == turn)
                {
                    bool valid = piece[index].movePiece(e.GetPosition(ChessBoard));
                    if(valid) turn = !turn;
                }
                
                Point p = piece[index].locationOnBoard();

                Canvas.SetLeft(piece[index].PieceImage, p.X);
                Canvas.SetTop(piece[index].PieceImage, p.Y);
                
                index = -1;
            }
        }
    }
}
