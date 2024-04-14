using System;
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

    private bool cancelEarlyPath;
    private bool isMovingAlongPath;
    private IEnumerator moveEntity;

    private void Awake()
    {
        Tile.onPlayerTileClicked += MoveEntityToTile;
    }

    private void MoveEntityToTile(Tile tileClicked)
    {
        //if (cancelEarlyPath && moveEntity != null) StopCoroutine(moveEntity);

        //cancelEarlyPath = isMovingAlongPath;

        moveEntity = MoveEntity(tileClicked);
        StartCoroutine(moveEntity);
    }

    private IEnumerator MoveEntity(Tile tileClicked)
    {
        //yield return new WaitWhile(() => isMovingAlongPath || cancelEarlyPath);

        var movableCell = MapManager.GetCellAt(movable.transform);
        var tileCell = MapManager.GetCellAt(tileClicked.transform);

        var path = Pathfinder.FindPath(movable, movableCell, tileCell);

        if (showLogs && !path.Any()) print("no path found");
        if (!path.Any()) yield break;

        var moveCoroutine = MoveAlongPath(movable, path);

        StartCoroutine(moveCoroutine);
    }

    private IEnumerator MoveAlongPath(IMovableAlongPath movable, IEnumerable<IWalkable> path)
    {
        isMovingAlongPath = true;

        IWalkable previousWalkable = null;
        foreach (IWalkable walkable in path)
        {
            previousWalkable?.OnMovableWalkOff(movable);
            movable.Move(walkable.GetPosition());

            yield return new WaitWhile(() => !movable.HasReachPos());

            walkable.OnMovableWalkOn(movable);
            previousWalkable = walkable;

            if (cancelEarlyPath) yield break;
        }

        isMovingAlongPath = false;
    }

    private void OnDestroy()
    {
        Tile.onPlayerTileClicked -= MoveEntityToTile;
    }
}
