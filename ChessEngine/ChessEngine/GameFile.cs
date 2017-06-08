using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    class GameFile
    {
        private String filePath;

        public GameFile(string filePath)
        {
            this.filePath = filePath;
            createPgn();
        }

        private void createPgn()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        public void updatePgn()
        {
            StreamWriter writer = new StreamWriter(filePath);

        }

        public void readPgn()
        {
            StreamReader reader = new StreamReader(filePath);
        }
    }
}
