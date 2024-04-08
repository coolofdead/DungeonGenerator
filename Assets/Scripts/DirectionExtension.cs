using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionExtension
{
    private static Dictionary<Direction, Vector2> directionToVector = new Dictionary<Direction, Vector2>()
    {
        { Direction.Down, Vector2.down },
        { Direction.Up, Vector2.up },
        { Direction.Right, Vector2.right },
        { Direction.Left, Vector2.left },
    };

    public static Vector2Int ToVector(this Direction direction)
    {
        Vector2Int directionVectorInt = Vector2Int.RoundToInt(directionToVector[direction]);
        return directionVectorInt;
    }
}
