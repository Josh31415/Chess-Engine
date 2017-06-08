using System.Windows;

namespace ChessEngine
{
    interface IGame
    {
        Piece[] Pieces { get; set; }
        void SetupBoard();
        void CheckMove(Point position, int index);
    }
}
