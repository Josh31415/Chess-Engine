using System;
using System.Collections.Generic;
using System.Windows;

namespace ChessEngine
{
    class CvsHGame : Game, IGame
    {
        public CvsHGame()
        {
            SetupBoard();
            turn = true;
            setCheck(false, false);
            gameFile = new GameFile("../../pgns/temp.pgn");
        }

        public Piece[] updatePieces(Piece[] currMoves, Piece moved, Point newLoc)
        {
            for(int i = 0; i < currMoves.Length; i++)
            {
                if(currMoves[i].Equals(moved)) currMoves[i].Location = newLoc;
            }

            return currMoves;
        }

        // Builds the move tree
        public void buildMoveStructure(int depth, int width, MoveNode head)
        {
            float eval = BoardEval();

            PieceMove[] moves = GameEvaluation.getMoveList(width, head.Data.Pieces);

            for (int i = 0; i < moves.Length; i++)
            {
                Piece[] newPieces = updatePieces(head.Data.Pieces, moves[i].piece, moves[i].newLocation);
                MoveNode newNode = new MoveNode(head, moves[i], new List<MoveNode>());
                head.addChild(newNode);

                if(depth < 1) return;
                buildMoveStructure(depth - 1, 5, newNode);
            }
        }

        public MoveTree buildMoveTree()
        {
            PieceMove initialMove = new PieceMove();
            Piece[] tempPieces = (Piece[])Pieces.Clone();
            initialMove.Pieces = tempPieces;

            MoveNode head = new MoveNode(null, initialMove, new List<MoveNode>());
            buildMoveStructure(3, 5, head);

            return new MoveTree(head, 3, 5);
        }

        public new bool CheckMove(Point position, int index)
        {
            if (base.CheckMove(position, index))
            {
                MoveTree tree = buildMoveTree();
                PieceMove bestMove = GameEvaluation.findBestMove(true, tree);

                return true;
            }
            else
            {
                return false;
            }
        }

        public float BoardEval()
        {
            Console.WriteLine("next turn");
            float eval = GameEvaluation.getBoardEvaluation(Pieces, this.getCheck());
            Console.WriteLine("Eval completed");

            return eval;
        }
    }
}
