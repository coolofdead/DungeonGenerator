using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonWallGenerator : MonoBehaviour
{
    public bool showLogs;

    public void GenerateWalls(Dungeon dungeon)
    {
        var directions = new List<Vector2Int>() { Vector2Int.down, Vector2Int.right, Vector2Int.left, Vector2Int.up };
        var nbWallsCreated = 0;

        foreach (var cell in dungeon.GetCells().ToArray())
        {
            foreach (var direction in directions)
            {
                var isCell = dungeon.HasAt(cell.GetDungeonPosition() + direction);

                if (isCell) continue;

                nbWallsCreated++; // Only for debug
                dungeon.Add(new Cell() { tileType = TileType.Wall, pos = cell.GetDungeonPosition() + direction });
            }
        }

        if (showLogs) print($"generated {nbWallsCreated}");
    }
}
