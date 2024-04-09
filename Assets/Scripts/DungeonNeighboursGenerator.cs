using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonNeighboursGenerator : MonoBehaviour
{
    public bool showLogs;

    public void SetTilesNeighbours(Dungeon dungeon)
    {
        foreach (Cell cell in dungeon)
        {
            var neighbours = dungeon.GetCells().Where((dungeonCell) => {
                return (dungeonCell.GetDungeonPosition().x == cell.GetDungeonPosition().x + 1 && dungeonCell.GetDungeonPosition().y == cell.GetDungeonPosition().y)
                    || (dungeonCell.GetDungeonPosition().x == cell.GetDungeonPosition().x - 1 && dungeonCell.GetDungeonPosition().y == cell.GetDungeonPosition().y)
                    || (dungeonCell.GetDungeonPosition().y == cell.GetDungeonPosition().y + 1 && dungeonCell.GetDungeonPosition().x == cell.GetDungeonPosition().x)
                    || (dungeonCell.GetDungeonPosition().y == cell.GetDungeonPosition().y - 1 && dungeonCell.GetDungeonPosition().x == cell.GetDungeonPosition().x);
            });

            if (showLogs) print(neighbours.Count());

            cell.SetNeighbours(neighbours.Cast<IWalkable>());
        }
    }
}
