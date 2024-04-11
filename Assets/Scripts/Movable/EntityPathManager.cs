using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityPathManager : MonoBehaviour
{
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
        StartCoroutine(MoveAlongPath(movable, path));
    }

    private IEnumerator MoveAlongPath(IMovableAlongPath movable, IEnumerable<IWalkable> path)
    {
        if (!path.Any())
        {
            yield break;
        }

        foreach (IWalkable walkable in path)
        {
            movable.Move(walkable.GetPosition());

            yield return new WaitWhile(() => !movable.HasReachPos());

            walkable.OnMovableWalkOn(movable);
        }
    }

    private void OnDestroy()
    {
        Tile.onPlayerTileClicked -= MoveEntitiesToTile;
    }
}
