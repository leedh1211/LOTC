using UnityEngine;
using UnityEngine.Tilemaps;

namespace Monster.AI
{
    /// <summary>
    /// Tilemap으로부터 “walkable” 격자를 만들고
    /// 월드좌표 ↔ 그리드좌표 변환을 제공합니다.
    /// </summary>
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        public float cellSize => tilemap.layoutGrid.cellSize.x;
        private Node[,] grid;
        private Vector3Int origin;

        private void Awake()
        {
            if (tilemap == null) Debug.LogError("GridManager.tilemap이 할당되지 않았습니다!");
            InitGridFromTilemap();
        }

        private void InitGridFromTilemap()
        {
            // 타일맵 상에 실제 타일이 깔린 최소/최대 영역 가져오기
            var bounds = tilemap.cellBounds;
            origin = bounds.min;

            int width  = bounds.size.x;
            int height = bounds.size.y;
            grid = new Node[width, height];

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var cellPos  = new Vector3Int(origin.x + x, origin.y + y, 0);
                bool hasTile = tilemap.HasTile(cellPos);
                bool walkable = !hasTile; // 타일이 있으면 장애물로 간주

                grid[x, y] = new Node(new Vector2Int(x, y), walkable);
            }
        }

        /// <summary>
        /// 월드좌표 → 격자 노드
        /// </summary>
        public Node GetNodeFromWorld(Vector2 worldPos)
        {
            if (grid == null)
            {
                Debug.LogError("GridManager.grid가 null입니다. InitGridFromTilemap()이 호출되었는지 확인하세요.");
                return null;
            }

            Vector3Int cell = tilemap.WorldToCell(worldPos);
            int ix = cell.x - origin.x;
            int iy = cell.y - origin.y;
            ix = Mathf.Clamp(ix, 0, grid.GetLength(0) - 1);
            iy = Mathf.Clamp(iy, 0, grid.GetLength(1) - 1);
            return grid[ix, iy];
        }

        /// <summary>
        /// 격자좌표 → 월드좌표 (셀 중앙)
        /// </summary>
        public Vector2 GridToWorld(Vector2Int gridPos)
        {
            var cell = new Vector3Int(origin.x + gridPos.x, origin.y + gridPos.y, 0);
            return tilemap.GetCellCenterWorld(cell);
        }

        /// <summary>
        /// 내부 그리드 배열 반환 (A*에 사용)
        /// </summary>
        public Node[,] GetGrid() => grid;
    }
}
