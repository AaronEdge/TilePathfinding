using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TilePathFinder;

public class PathfinderDemo : MonoBehaviour
{

    public Grid TileGrid;
    public Pathfinder pathFinder;

    void Start()
    {
        pathFinder = new Pathfinder(TileGrid, "Blocking", "Floor");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int endPoint = TileGrid.WorldToCell(mousePos);
            Vector3Int startPoint = TileGrid.WorldToCell(transform.position);

            List<Vector3Int> path = pathFinder.FindPath(startPoint, endPoint);

            int steps = 0;
            foreach (Vector3Int step in path)
            {
                steps++;
                Debug.Log("step " + steps + ": (" + step.x + "," + step.y + ")");
                transform.position = step;
            }
        }
    }
}
