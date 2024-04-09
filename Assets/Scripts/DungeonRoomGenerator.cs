using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonRoomGenerator : MonoBehaviour
{
    public bool showLogs;

    public void GenerateRooms(Dungeon dungeon, DungeonData dungeonData, System.Random rnd)
    {
        var roomData = dungeonData.DungeonRoomData;

        var totalRooms = rnd.Next(roomData.minRoom, roomData.maxRoom);
        if (showLogs) print($"Total room generated {totalRooms}");

        var rooms = new List<Vector2Int>();
        for (int i = 0; i < totalRooms; i++)
        {
            var roomWidth = rnd.Next(roomData.minRoomWidth, roomData.maxRoomWidth);
            var roomLength = rnd.Next(roomData.minRoomLength, roomData.maxRoomLength);
            if (showLogs) print($"room {i} width {roomWidth} room length {roomLength}");

            var spaceBetweenRoom = rnd.Next(roomData.minSpaceBetweenRoom, roomData.maxSpaceBetweenRoom);
            Vector2Int roomCoordonate = i == 0 ? Vector2Int.zero : GetRandomPointWithDistance(rooms, spaceBetweenRoom + (roomWidth + roomLength) / 2, rnd);

            Room room = new(roomWidth, roomLength);
            room.AddCells(roomCoordonate);
            rooms.Add(roomCoordonate);

            dungeon.Add(room);
        }
    }

    private Vector2Int GetRandomPointWithDistance(IEnumerable<Vector2Int> cellsPlaced, int distance, System.Random rnd)
    {
        // Choix aleatoire d'un point dans la liste
        var randomPoint = cellsPlaced.ElementAt(rnd.Next(0, cellsPlaced.Count() - 1));
        var randomDir = new Vector2((float)((rnd.NextDouble() * 2.0) - 1.0), (float)((rnd.NextDouble() * 2.0) - 1.0));

        Vector2Int newPoint = Vector2Int.RoundToInt(randomPoint + distance * randomDir);

        if (cellsPlaced.All(cellPos => Vector2Int.Distance(newPoint, cellPos) > distance))
            return newPoint;
        else
            return GetRandomPointWithDistance(cellsPlaced, distance, rnd); // Ressayer avec un nouveau point
    }
}
