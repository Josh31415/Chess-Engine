using System.Collections.Generic;

namespace ChessEngine
{
    class MoveTree
    {
        MoveNode head;
        MoveNode currNode;
        int depth;

        public MoveTree(PieceMove headData)
        {
            head = new MoveNode(null, headData, null);
            depth = 0;
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public void addLevel(MoveNode parent, PieceMove[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                parent.addChild(new MoveNode(parent, data[i], null));
            }

            depth++;
        }

        public void addCurrChildren(PieceMove[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                currNode.addChild(new MoveNode(currNode, data[i], null));
            }
        }

        public List<MoveNode> getNextLevel()
        {
            return currNode.getChildNodes();
        }

        public bool moveDown(int index)
        {
            MoveNode[] nodes = currNode.getChildNodes().ToArray();
            if(nodes.Length <= index && !nodes[index].Equals(null)) {
                currNode = nodes[index];
                return true;
            }

            return false;
        }

        public bool moveRight()
        {
            int index = -1;
            MoveNode par = currNode.Parent;
            MoveNode[] nodes = par.getChildNodes().ToArray();

            for(int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == currNode) index = i;
            }

            if(index != -1 && index > 0)
            {
                currNode = nodes[index - 1];
                return true;
            }

            return false;
        }

        public bool moveLeft()
        {
            int index = -1;
            MoveNode par = currNode.Parent;
            MoveNode[] nodes = par.getChildNodes().ToArray();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == currNode) index = i;
            }

            if (index != -1 && index < nodes.Length)
            {
                currNode = nodes[index + 1];
                return true;
            }

            return false;
        }

        public bool moveUp()
        {
            if(!currNode.Parent.Equals(null))
            {
                this.currNode = currNode.Parent;
            }

            return false;
        }
    }
}
