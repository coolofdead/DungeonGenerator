using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonCorridorGenerator : MonoBehaviour
{
    public bool showLogs;

    public void ConnectRooms(Dungeon dungeon, DungeonData dungeonData, System.Random rnd)
    {
        foreach (Room room in dungeon.Rooms)
        {
            var connectedRoom = dungeon.Rooms.Where(r => !r.Equals(room)).OrderBy(_ => rnd.Next()).First();
            var fromRoom = room.cells.OrderBy(_ => rnd.Next()).First();
            var toRoom = connectedRoom.cells.OrderBy(_ => rnd.Next()).First();

            if (showLogs) print($"connecting room {fromRoom.GetDungeonPosition()} with {toRoom.GetDungeonPosition()}");

            ConnectRoomToRoom(dungeon, fromRoom, toRoom);
        }
    }

    private void ConnectRoomToRoom(Dungeon dungeon, Cell roomStartCell, Cell roomEndCell)
    {
        var xDistance = Math.Abs(roomEndCell.pos.x - roomStartCell.pos.x);
        for (int i = 0; i < xDistance; i++)
        {
            int x = Mathf.RoundToInt(Mathf.Lerp(roomStartCell.pos.x, roomEndCell.pos.x, i / ((float)xDistance)));
            dungeon.Add(new Cell() { tileType = TileType.Ground, pos = new Vector2Int(x, roomStartCell.pos.y) });
        }

        var yDistance = Math.Abs(roomEndCell.pos.y - roomStartCell.pos.y);
        for (int i = 0; i < yDistance; i++)
        {
            int y = Mathf.RoundToInt(Mathf.Lerp(roomStartCell.pos.y, roomEndCell.pos.y, i / ((float)yDistance)));
            dungeon.Add(new Cell() { tileType = TileType.Ground, pos = new Vector2Int(roomEndCell.pos.x, y) });
        }
    }
}
