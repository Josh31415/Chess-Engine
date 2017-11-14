using System.Windows;

namespace ChessEngine
{
    class HvsHGame : Game, IGame
    {
        private Piece[] piece = new Piece[32];
        private bool turn;
        private Check check;
        GameFile file;

        public HvsHGame()
        {
            SetupBoard();
            check.isCheck = false;
            file = new GameFile("../../pgns/temp.pgn");
        }

        public bool CheckMove(Point position, int index)
        {
            if (index != -1)
            {
                if (piece[index].Color == turn)
                {
                    Point newLoc = position;
                    newLoc = Piece.boardToPiece(newLoc);
                    bool valid = piece[index].movePiece(newLoc);
                    bool nBlocked = Rules.checkPiecePath(piece[index], newLoc, piece);

                    if(check.isCheck && check.checkColor == turn)
                    {
                        Piece tempPiece = (Piece)piece[index].Clone();
                        tempPiece.Location = newLoc;
                        if (Rules.isCheck(piece, piece[index].AttackedSquares(piece), turn)) return false;
                    }

                    if (valid && nBlocked)
                    {
                        if (Rules.CheckCapture(newLoc, piece[index], piece, out int capIndex))
                        {
                            piece[capIndex].Captured = true;
                        }
                        piece[index].Location = newLoc;
                        piece[index].Moved = true;
                        bool ischeck = Rules.isCheck(piece, piece[index].AttackedSquares(piece), turn);
                        check.isCheck = ischeck;
                        check.checkColor = turn;
                        
                        pieceMove move;
                        move.piece = piece[index];
                        move.newLocation = newLoc;
                        move.capture = piece[capIndex].Captured;
                        move.check = ischeck;
                        file.updatePgn(move);

                        return true;
                    }
                    else if (!valid && nBlocked && piece[index].GetType().Equals(typeof(King)))
                    {
                        Piece rook = new Rook();
                        bool check = Rules.checkCastle(piece[index], newLoc, piece, out int location);

                        if (check)
                        {
                            Point rLoc = piece[location].Location;
                            if (rLoc.X - piece[index].Location.X < 0)
                            {
                                rLoc.X = newLoc.X + 1;
                            }
                            else if (rLoc.X - piece[index].Location.X > 0)
                            {
                                rLoc.X = newLoc.X - 1;
                            }
                           
                            pieceMove move;
                            move.piece = piece[index];
                            move.newLocation = newLoc;
                            move.capture = false;
                            move.check = true;
                            file.updatePgn(move);

                            piece[location].Location = rLoc;
                            piece[index].Location = newLoc;
                            piece[location].Moved = true;
                            piece[index].Moved = true;
                            
                            return true;
                        }
                    }
                    else if (!valid && piece[index].GetType().Equals(typeof(Pawn)))
                    {
                        bool pawnCap = Rules.CheckPawnCapture(newLoc, piece, index, out int capIndex);

                        if (pawnCap)
                        {
                            piece[capIndex].Captured = true;
                            pieceMove move;

                            bool ischeck = Rules.isCheck(piece, piece[index].AttackedSquares(piece), turn);
                            check.isCheck = ischeck;
                            check.checkColor = turn;

                            move.piece = piece[index];
                            move.newLocation = newLoc;
                            move.capture = false;
                            move.check = false;
                            file.updatePgn(move);

                            piece[index].Location = newLoc;
                            
                            return true;
                        }
                    }

                    index = -1;
                }
            }

            return false;
        }

        public bool NextTurn()
        {
            turn = !turn;
            return true;
        }
    }
}
