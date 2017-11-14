using System.Windows;

namespace ChessEngine
{
    interface IGame
    {
        Piece[] Pieces { get; set; }
        void SetupBoard();
        bool CheckMove(Point position, int index);
        bool NextTurn();
    }
}
