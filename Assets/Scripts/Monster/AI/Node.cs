using UnityEngine;

namespace Monster.AI
{
    public class Node
    {
        public Vector2Int pos;
        public bool walkable;
        public float gCost;
        public float hCost;
        public float fCost => gCost + hCost;
        public Node parent;

        public Node(Vector2Int pos, bool walkable)
        {
            this.pos      = pos;
            this.walkable = walkable;
            this.gCost    = Mathf.Infinity;
            this.hCost    = 0f;
            this.parent   = null;
        }
    }
}