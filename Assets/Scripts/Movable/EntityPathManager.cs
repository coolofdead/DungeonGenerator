using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityPathManager : MonoBehaviour
{
    public Pathfinder Pathfinder;
    public MapManager MapManager;
    public MovableEntity movableAlongPath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var startCell = MapManager.Dungeon[Random.Range(0, MapManager.Dungeon.Cells.Count)];
            var endCell = MapManager.Dungeon[Random.Range(0, MapManager.Dungeon.Cells.Count)];

            //print($"from {startCell.GetPosition()}");
            //print($"to {endCell.GetPosition()}");
            var path = Pathfinder.FindPath(startCell, endCell);

            //print(path.Count());
            StartCoroutine(MoveAlongPath(movableAlongPath, path));
        }
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
        }
    }
}
