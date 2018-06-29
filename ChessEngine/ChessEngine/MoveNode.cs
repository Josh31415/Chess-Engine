using System;
using System.Collections.Generic;

namespace ChessEngine
{
    class MoveNode
    {
        private PieceMove data;
        private MoveNode parent;
        private List<MoveNode> childNodes;

        public MoveNode(MoveNode parent, PieceMove data, List<MoveNode> children)
        {
            this.parent = parent;
            this.data = data;
            this.childNodes = children;
        }

        public PieceMove Data
        {
            get { return data; }
            set { data = value; }
        }

        public MoveNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private void checkChildrenNull()
        {
            if (childNodes.Equals(null)) new NullReferenceException("Child nodes is null");
        }

        public List<MoveNode> getChildNodes()
        {
            checkChildrenNull();
            return childNodes;
        }

        public void setChildNodes(List<MoveNode> children)
        {
            this.childNodes = children;
        }

        public void addChild(MoveNode node)
        {
            checkChildrenNull();
            childNodes.Add(node);
        }

        public void removeChild(MoveNode node)
        {
            checkChildrenNull();
            childNodes.Remove(node);
        }
    }
}
