// AStar.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.AI
{
    public static class AStar
    {
        public static List<Vector2Int> FindPath(Node start, Node goal, Node[,] grid, Func<Vector2Int, bool> isWalkable)
        {
            foreach (var node in grid)
            {
                node.gCost = Mathf.Infinity;
                node.hCost = 0f;
                node.parent = null;
            }

            start.gCost = 0f;
            start.hCost = Heuristic(start, goal);

            var openSet = new List<Node> { start };
            var closedSet = new HashSet<Node>();

            while (openSet.Count > 0)
            {
                Node current = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    var n = openSet[i];
                    if (n.fCost < current.fCost || (n.fCost == current.fCost && n.hCost < current.hCost))
                        current = n;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                if (current == goal)
                    return RetracePath(current);

                foreach (var neighbor in GetNeighbors(current, grid))
                {
                    if (!neighbor.walkable || !isWalkable(neighbor.pos) || closedSet.Contains(neighbor))
                        continue;

                    float newG = current.gCost + Vector2Int.Distance(current.pos, neighbor.pos);
                    if (newG < neighbor.gCost)
                    {
                        neighbor.gCost = newG;
                        neighbor.hCost = Heuristic(neighbor, goal);
                        neighbor.parent = current;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            return null;
        }

        private static float Heuristic(Node a, Node b)
        {
            return Vector2Int.Distance(a.pos, b.pos);
        }

        private static List<Node> GetNeighbors(Node node, Node[,] grid)
        {
            var neighbors = new List<Node>();
            var dirs = new[]
            {
                Vector2Int.up, Vector2Int.down,
                Vector2Int.left, Vector2Int.right
            };
            foreach (var d in dirs)
            {
                var check = node.pos + d;
                if (check.x >= 0 && check.y >= 0 && check.x < grid.GetLength(0) && check.y < grid.GetLength(1))
                    neighbors.Add(grid[check.x, check.y]);
            }
            return neighbors;
        }

        private static List<Vector2Int> RetracePath(Node end)
        {
            var path = new List<Vector2Int>();
            for (var node = end; node != null; node = node.parent)
                path.Add(node.pos);
            path.Reverse();
            return path;
        }
    }
}
