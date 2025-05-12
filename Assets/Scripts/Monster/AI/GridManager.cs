using UnityEngine;
using UnityEngine.Tilemaps;

namespace Monster.AI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        public float cellSize => tilemap.layoutGrid.cellSize.x;
        private Node[,] grid;
        private Vector3Int origin;

        private void Awake()
        {
            InitGridFromTilemap();
        }

        private void InitGridFromTilemap()
        {
            var bounds = tilemap.cellBounds;
            origin = bounds.min;
            int width = bounds.size.x;
            int height = bounds.size.y;
            grid = new Node[width, height];

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var cellPos = new Vector3Int(origin.x + x, origin.y + y, 0);
                bool hasTile = tilemap.HasTile(cellPos);
                bool walkable = !hasTile;
                grid[x, y] = new Node(new Vector2Int(x, y), walkable);
            }
        }

        public Node GetNodeFromWorld(Vector2 worldPos)
        {
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            int ix = cell.x - origin.x;
            int iy = cell.y - origin.y;
            ix = Mathf.Clamp(ix, 0, grid.GetLength(0) - 1);
            iy = Mathf.Clamp(iy, 0, grid.GetLength(1) - 1);
            return grid[ix, iy];
        }

        public Vector2 GridToWorld(Vector2Int gridPos)
        {
            var cell = new Vector3Int(origin.x + gridPos.x, origin.y + gridPos.y, 0);
            return tilemap.GetCellCenterWorld(cell);
        }

        public bool IsWalkableForSize(Vector2Int gridPos, int monsterSize)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            for (int dx = 0; dx < monsterSize; dx++)
            for (int dy = 0; dy < monsterSize; dy++)
            {
                int checkX = gridPos.x + dx;
                int checkY = gridPos.y + dy;
                if (checkX < 0 || checkY < 0 || checkX >= width || checkY >= height)
                    return false;
                if (!grid[checkX, checkY].walkable)
                    return false;
            }
            return true;
        }

        public Node[,] GetGrid() => grid;
    }
}
