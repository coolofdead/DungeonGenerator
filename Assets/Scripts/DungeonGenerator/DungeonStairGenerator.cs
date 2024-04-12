using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonStairGenerator : MonoBehaviour
{
    public bool showLogs;

    public void GenerateStair(Dungeon dungeon, System.Random rnd)
    {
        var stairRoom = dungeon.Rooms.Where(room => !room.isDummyRoom).OrderBy(_ => rnd.Next()).First();
        var roomCellEdges = stairRoom.GetEdges(); // Buffer
        var availableCells = stairRoom.cells.Where(cell => !roomCellEdges.Contains(cell));

        var stairCell = availableCells.ElementAt(rnd.Next(0, availableCells.Count()));
        stairCell.HasStair = true;
    }
}
