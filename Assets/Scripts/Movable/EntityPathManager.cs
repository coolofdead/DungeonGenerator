using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityPathManager : MonoBehaviour
{
    public bool showLogs;

    public Pathfinder Pathfinder;
    public MapManager MapManager;

    public MovableEntity movable;

    private void Awake()
    {
        Tile.onPlayerTileClicked += MoveEntitiesToTile;
    }

    private void MoveEntitiesToTile(Tile tileClicked)
    {
        var movableCell = MapManager.GetCellAt(movable.transform);
        var tileCell = MapManager.GetCellAt(tileClicked.transform);

        var path = Pathfinder.FindPath(movable, movableCell, tileCell);
        
        if (showLogs) print(path.Count());
        StartCoroutine(MoveAlongPath(movable, path));
    }

    private IEnumerator MoveAlongPath(IMovableAlongPath movable, IEnumerable<IWalkable> path)
    {
        if (!path.Any())
        {
            if (showLogs) print("no path found");
            yield break;
        }

        IWalkable previousWalkable = null;
        foreach (IWalkable walkable in path)
        {
            previousWalkable?.OnMovableWalkOff(movable);
            movable.Move(walkable.GetPosition());

            yield return new WaitWhile(() => !movable.HasReachPos());

            walkable.OnMovableWalkOn(movable);
            previousWalkable = walkable;
        }
    }

    private void OnDestroy()
    {
        Tile.onPlayerTileClicked -= MoveEntitiesToTile;
    }
}
