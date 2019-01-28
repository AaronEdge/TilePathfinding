# TilePathfinding

Path finder for unitys tile system.

With this script you have the ability to create your tilemap using unity's built in tile mapping tools and have A* pathfinding for gameobjects on the grid.

## Getting Started

These instructions will get tilebased A* pathfinding up and running in your unity project.

### Prerequisites

Requires Unity 2017.2 or newer.

You will need to add two new tags to your project: 
One for tiles that block pathfinding (set to "Blocking" by default)
and one that indicates a tile is walkable (set to "Floor" by default).

You will need to import these three files into your unity project:
[Node.cs](Node.cs)
[NodeGrid.cs](NodeGrid.cs)
[Pathfinder.cs](Pathfinder.cs)

### Quick start

1. Create your tilemap using unity's tools.
2. Tag the tile map layers that contain walkable floor tile with the "Floor" tag.
3. Tag the tile map layers that contain the tiles that block the path with the "Blocking" tag.
4. Attach the [PathfinderDemo.cs](PathfinderDemo.cs) script to your player gameobject.
5. Add your tilemap Grid object to the TileGrid variable on the PathfinderDemo script using the inspector.

You can use the [PathfinderDemo.cs](PathfinderDemo.cs) as an example template to create your own pathfinding scripts.

### Using the script

You can access the pathfinder from any MonoBehaviour script by using the TilePathFinder namespace.

```cs
using TilePathFinder;
```

In the Start() function create and store a new pathFinder object passing it your tile grid object, a string containing your blocking layers tag and a string containing your floors tag.

```cs
pathFinder = new TilePathfinder(GridObject, "Blocking", "Floor");
```

This will create a nodegrid based on the tilemap layers found in the grid object.

Using the new pathfinder object you can now call the FindPath method to get an ordered list of tile locations that lead to the target location.
The FindPath method requires a starting point (Vector3Int) and an endpoint (Vector3Int) as input, and will return an ordered list of points that (List<Vector3Int>) make a path from the start point to the endpoint.

```cs
List<Vector3Int> path = TilePathFinder.FindPath(startPoint, endPoint);
```

The unity method WorldToCell() can be used to convert a world position to a cell position (Vector3Int) to be used as input.

```cs
Vector3Int startPoint = TileGrid.WorldToCell(transform.position);
```

## Author

* **Aaron Edge** - *Initial work*

See also the list of [contributors](https://github.com/AaronEdge/TilePathfinding/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* Based on SebLague's A* pathfinding available [here](https://github.com/SebLague/Pathfinding).

