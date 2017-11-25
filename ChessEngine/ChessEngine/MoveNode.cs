using System.Collections.Generic;

namespace ChessEngine
{
    class MoveNode
    {
        private pieceMove data;
        private MoveNode parent;
        private List<MoveNode> childNodes;

        public MoveNode(MoveNode parent, pieceMove data, List<MoveNode> children)
        {
            this.parent = parent;
            this.data = data;
            this.childNodes = children;
        }

        public pieceMove Data
        {
            get { return data; }
            set { data = value; }
        }

        public MoveNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public List<MoveNode> getChildNodes()
        {
            return childNodes;
        }

        public void setChildNodes(List<MoveNode> children)
        {
            this.childNodes = children;
        }

        public void addChild(MoveNode node)
        {
            childNodes.Add(node);
        }

        public void removeChild(MoveNode node)
        {
            childNodes.Remove(node);
        }
    }
}
