using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PathfindingGrid : MonoBehaviour
    {
        public Transform Player;
        public LayerMask unwalkableMasks;
        public Vector2 gridWorldSize;
        public float nodeRadius;

        Node[,] grid;
        float nodeDiameter;
        int gridSizeX, gridSizeY;

        private void Start()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
            grid = new Node[gridSizeX, gridSizeY];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0  && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        public void CreateGrid()
        {           
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMasks));
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
            int x = Mathf.RoundToInt((gridSizeX) * percentX);
            int y = Mathf.RoundToInt((gridSizeY) * percentY)-1;
            return grid[x, y];
        }

        public List<Node> path;
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
            if (grid != null)
            {
                Node playerNode = NodeFromWorldPoint(Player.position
                    );
                foreach (Node n in grid)
                {
                    if (playerNode.worldPosition == n.worldPosition)
                    {
                        Gizmos.color = Color.cyan;   
                    }
                    else
                    {
                        Gizmos.color = (n.walkable) ? Color.white : Color.red;
                        if (path != null)
                            if (path.Contains(n))
                                Gizmos.color = Color.black;
                    }
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}

