using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCorridorGenerator : MonoBehaviour
{
    public bool showLogs;

    public void ConnectRooms(Dungeon dungeon, DungeonData dungeonData, System.Random rnd)
    {
        var corridorData = dungeonData.DungeonCorridorData;

        foreach (Room room in dungeon.Rooms)
        {
            var connectedRoom = dungeon.Rooms.Where(r => !r.Equals(room)).OrderBy(_ => rnd.Next()).First();

            var fromRoom = room.GetEdges().OrderBy(_ => rnd.Next()).First();
            var toRoom = connectedRoom.GetEdges().OrderBy(_ => rnd.Next()).First();

            if (showLogs) print($"connecting room {fromRoom.GetDungeonPosition()} with {toRoom.GetDungeonPosition()}");

            ConnectRoomToRoom(dungeon, corridorData, fromRoom, toRoom, rnd);
        }
    }

    private void ConnectRoomToRoom(Dungeon dungeon, DungeonCorridorData corridorData, Cell roomStartCell, Cell roomEndCell, System.Random rnd)
    {
        var dir = roomEndCell.pos - roomStartCell.pos;
        var pathPos = roomStartCell.pos;
        var moveOnX = rnd.Next(0, 100) > 50;
        //var nbCorridorCorner = corridorData.nbCorridorCorner;
        var changeDirAtMagnitude = dir.magnitude > 3 ? rnd.Next(2, (int)dir.magnitude - 2) : 1;

        while (dir.magnitude > 1)
        {
            var moveDirClamped = (moveOnX ? Vector2Int.right * dir : Vector2Int.up * dir);
            moveDirClamped.Clamp(-Vector2Int.one, Vector2Int.one);

            pathPos += moveDirClamped;
            dir = roomEndCell.pos - pathPos;
            dungeon.Add(new Cell(pathPos, TileType.Ground, isImmutable: true));

            moveOnX = (!moveOnX && dir.y == 0) || (moveOnX && dir.x != 0);
            moveOnX = changeDirAtMagnitude == 0 || changeDirAtMagnitude == -1 ? !moveOnX : moveOnX; // Make it change dir at least twice during the path creation
            changeDirAtMagnitude--; // Make it change dir at least twice during the path creation
        }
    }
}
