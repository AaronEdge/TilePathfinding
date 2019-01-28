using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace TilePathFinder
{
    public class NodeGrid
    {
        public NodeGrid(Grid _TileGrid, string _wallTag, string _FloorTag)
        {
            tileGrid = _TileGrid;
            wallTag = _wallTag;
            floorTag = _FloorTag;
            if (tileGrid != null && wallTag != "")
                CreateGrid();
        }
        public Grid tileGrid;
        public string wallTag = "Blocking";
        public string floorTag = "Floor";
        public Vector2Int vGridWorldSize;
        public Node[,] nodeArray;
        public int xOffset;
        public int yOffset;

        public void CreateGrid()
        {
            Tilemap[] allTileMaps = tileGrid.GetComponentsInChildren<Tilemap>();
            // Check for largest map
            int largestX = 0, largestY = 0;
            foreach (Tilemap map in allTileMaps)
            {
                map.CompressBounds();
                BoundsInt bounds = map.cellBounds;
                if (bounds.size.x > largestX)
                    largestX = map.size.x;
                if (bounds.size.y > largestY)
                    largestY = map.size.y;
                if (map.cellBounds.xMin < xOffset)
                    xOffset = map.cellBounds.xMin;
                if (map.cellBounds.yMin < yOffset)
                    yOffset = map.cellBounds.yMin;
            }
            nodeArray = new Node[largestX, largestY];
            vGridWorldSize.x = largestX;
            xOffset = xOffset * -1;
            yOffset = yOffset * -1;
            vGridWorldSize.y = largestY;
            foreach (Tilemap map in allTileMaps)
            {
                foreach (var pos in map.cellBounds.allPositionsWithin)
                {
                    int gridPosX = pos.x + xOffset;
                    int gridPosY = pos.y + yOffset;
                    if (nodeArray[gridPosX, gridPosY] == null)
                    {
                        if (map.tag == wallTag)
                        {
                            nodeArray[gridPosX, gridPosY] = new Node(gridPosX, gridPosY, false);
                        }
                        else if (map.tag == floorTag && map.GetTile(new Vector3Int(pos.x, pos.y, 0)) != null)
                        {
                            nodeArray[gridPosX, gridPosY] = new Node(gridPosX, gridPosY, true);
                        }
                    }
                    else if (map.tag == wallTag || nodeArray[gridPosX, gridPosY].walkable == false || map.tag != floorTag)
                    {
                        nodeArray[gridPosX, gridPosY].walkable = false;
                    }
                }
            }
        }
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < vGridWorldSize.x && checkY >= 0 && checkY < vGridWorldSize.y)
                    {
                        if (nodeArray[checkX, checkY] != null && nodeArray[checkX, checkY].walkable)
                            neighbours.Add(nodeArray[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
    }
}