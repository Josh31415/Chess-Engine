using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Timers;
using System.Media;

namespace ChessEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private int index;
        private bool turn;
        Board board;
        IGame game;
        private SoundPlayer startSoundPlayer = new System.Media.SoundPlayer("../../Sounds/chessPieceSound.wav");

        public void setupGame()
        {
            game = new HvsHGame();
            
            for(int i = 0; i < 32; i++)
            {
                Point p = board.toBoardCorrdinates(game.Pieces[i].Location);
                Canvas.SetLeft(game.Pieces[i].PieceImage, p.X);
                Canvas.SetTop(game.Pieces[i].PieceImage, p.Y);
                ChessBoard.Children.Add(game.Pieces[i].PieceImage);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            board = new Board((int)ChessBoard.Width);
            setupGame();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point p = e.GetPosition(ChessBoard);
            index = board.findPiece(p, game.Pieces);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed && index != -1)
            {
                Point p = e.GetPosition(ChessBoard);

                int top = Canvas.GetZIndex(game.Pieces[index].PieceImage);
                foreach (Image child in ChessBoard.Children)
                {
                    if (top < Canvas.GetZIndex(child)) top = Canvas.GetZIndex(child);
                }

                p.X = p.X - 30;
                p.Y = p.Y - 30;
                Canvas.SetTop(game.Pieces[index].PieceImage, p.Y);
                Canvas.SetLeft(game.Pieces[index].PieceImage, p.X);
                Canvas.SetZIndex(game.Pieces[index].PieceImage, top + 1);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (index != -1)
            {
                bool valid = game.CheckMove(e.GetPosition(ChessBoard), index);

                if (valid) startSoundPlayer.Play();
                
                for (int i = 0; i < 32; i++)
                {
                    if (game.Pieces[i].Captured)
                    {
                        ChessBoard.Children.Remove(game.Pieces[i].PieceImage);
                    }
                    else
                    {
                        Point p = game.Pieces[index].locationOnBoard();
                        Canvas.SetLeft(game.Pieces[index].PieceImage, p.X);
                        Canvas.SetTop(game.Pieces[index].PieceImage, p.Y);
                    }

                }
            }
            
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if(GameSelect.SelectedValue.ToString() == "Player vs Player")
            {
                game = new HvsHGame();
            }

            Console.WriteLine(GameSelect.SelectedValue);
        }

        private void GameSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameSelect.Visibility = Visibility.Visible;
            Console.WriteLine(GameSelect.SelectedValue);
            GameSelect.IsDropDownOpen = true;
        }
    }
}