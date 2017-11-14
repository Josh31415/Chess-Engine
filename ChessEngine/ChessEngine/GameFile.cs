using System;
using System.IO;
using System.Windows;

namespace ChessEngine
{
    class GameFile
    {
        private String filePath;
        private int moveNum = 1;

        public GameFile(string filePath)
        {
            this.filePath = filePath;
            createPgn();
        }

        private void createPgn()
        { 
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            
            File.Create(filePath).Dispose();
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine("[Event \"Online Chess\"]");
                writer.WriteLine("[Site \"Computer\"]");
                writer.WriteLine("[Date \"" + DateTime.Now + "\"]");
                writer.WriteLine("[White \"Player 1\"]");
                writer.WriteLine("[Black \"Player 2\"]");
                writer.WriteLine("[Result \"\"]");
                writer.WriteLine("[ECO \"\"]");
                writer.WriteLine("[White Elo \"\"]");
                writer.WriteLine("[BlackElo \"\"]");
                writer.WriteLine("");
                writer.Close();
            }
        }

        private String moveLocation(Point p)
        {
            string move = "";
            if (p.X == 0) move = "a";
            else if (p.X == 1) move = "b";
            else if (p.X == 2) move = "c";
            else if (p.X == 3) move = "d";
            else if (p.X == 4) move = "e";
            else if (p.X == 5) move = "f";
            else if (p.X == 6) move = "g";
            else if (p.X == 7) move = "h";

            return move + p.Y.ToString();
        }

        private String pieceSymbol(Piece p)
        {
            string symbol = "";
            if (p is King) symbol = "K";
            else if (p is Queen) symbol = "Q";
            else if (p is Bishop) symbol = "B";
            else if (p is Knight) symbol = "N";
            else if (p is Rook) symbol = "R";
            else if (p is Pawn) symbol = "";

            return symbol;
        }

        private String getNormalMove(pieceMove p)
        {
            string move = moveLocation(p.newLocation);
            string symbol = pieceSymbol(p.piece);
            return symbol + move + " ";
        }

        private String getCaptureMove(pieceMove p)
        {
            string move = moveLocation(p.newLocation);
            string symbol = pieceSymbol(p.piece);
            return symbol + "x" + move;
        }

        private String getPgnValue(pieceMove p)
        {
            if (p.capture) return getCaptureMove(p);
            else if (p.check) return "O-O ";
            else return getNormalMove(p);
        }

        public void updatePgn(pieceMove p)
        {
            String move = getPgnValue(p);
            using (StreamWriter writer = File.AppendText(filePath))
            {
                if (p.piece.Color == true)
                {
                    writer.Write(moveNum + ". " + move);
                    moveNum++;
                }
                else
                {
                    writer.Write(move);
                }
                
            }
        }

        // Not finished
        public pieceMove[] readPgn(string path)
        {

            using (TextReader tr = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line.Contains("1."))
                    {
                        int xLoc = 0;
                        int yLoc = 0;
                        Type piece = null;

                        // split line between moves
                        string[] moves = line.Split(new char[0]);

                        for(int i = 0; i < moves.Length; i+=2)
                        {
                            // split between move number and move data
                            string[] whiteMove = moves[i].Split(new char[] { '.' });
                            string blackMove = moves[i + 1];
                            int moveNum = int.Parse(whiteMove[0]);

                            char[] loc = whiteMove[1].ToCharArray();

                            if(loc.Length == 2)
                            {
                                piece = typeof(Pawn);
                                xLoc = char.ToUpper(loc[0]) - 64;
                                yLoc = int.Parse(loc[1].ToString());
                            }
                            else if(loc.Length == 3)
                            {
                                piece = getPieceType(loc[0].ToString());
                                if(loc[1] == '-')
                                {
                                    xLoc = 6;
                                    yLoc = 7;
                                }
                                else
                                {
                                    xLoc = char.ToUpper(loc[1]) - 65;
                                    yLoc = int.Parse(loc[2].ToString());
                                }
                            }
                            else if(loc.Length == 4)
                            {
                                if(loc[1] == 'x')
                                {
                                    xLoc = char.ToUpper(loc[0]) - 64;
                                    yLoc = int.Parse(loc[1].ToString());
                                }
                            }

                            Console.WriteLine(piece.ToString());
                            Console.WriteLine(moveNum);
                            Console.WriteLine(xLoc);
                            Console.WriteLine(yLoc);
                            Console.WriteLine();
                        }


                    }
                }

            }
                    return null;
        }

        private Type getPieceType(string type)
        {
            type = type.ToUpper();
            Type pieceType;
            if (type == "K") pieceType = typeof(King);
            else if (type == "Q") pieceType = typeof(Queen);
            else if (type == "R") pieceType = typeof(Rook);
            else if (type == "B") pieceType = typeof(Bishop);
            else if (type == "N") pieceType = typeof(Knight);
            else if (type == "O") pieceType = typeof(King);
            else pieceType = null;

            return pieceType;
        }
    }
}
