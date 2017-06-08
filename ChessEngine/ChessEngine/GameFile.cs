using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                writer.WriteLine("[BlackElo \"\"]");
                writer.WriteLine("");
                writer.Close();
            }
        }

        private String getPgnValue(Piece p, Point newLoc)
        {
            string symbol = "";
            string move = "";
            string num = moveNum.ToString() + ".";

            if (p is King) symbol = "k";
            else if (p is Queen) symbol = "q";
            else if (p is Bishop) symbol = "b";
            else if (p is Knight) symbol = "n";
            else if (p is Rook) symbol = "r";
            else if (p is Pawn) symbol = "";

            if (newLoc.X == 0) move = "a";
            else if (newLoc.X == 1) move = "b";
            else if (newLoc.X == 2) move = "c";
            else if (newLoc.X == 3) move = "d";
            else if (newLoc.X == 4) move = "e";
            else if (newLoc.X == 5) move = "f";
            else if (newLoc.X == 6) move = "g";
            else if (newLoc.X == 7) move = "h";

            return num + symbol + move + newLoc.Y.ToString() + " ";
        }

        public void updatePgn(Piece p, Point newLoc)
        {
            String move = getPgnValue(p, newLoc);
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.Write(move);
            }
            moveNum++;
        }

        public void readPgn()
        {
            StreamReader reader = new StreamReader(filePath);
        }
    }
}
