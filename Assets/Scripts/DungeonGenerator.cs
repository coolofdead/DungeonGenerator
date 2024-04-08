using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;
using System.Drawing;

public class DungeonGenerator : MonoBehaviour
{
    public bool showLogs = false;

    private System.Random rnd;

    public Dungeon GenerateDungeon(MapGeneratorRules generationRules)
    {
        Dungeon dungeon = new(generationRules.MapSize);

        int seed = generationRules.seed == 0 ? UnityEngine.Random.Range(0, 9999) : generationRules.seed;
        rnd = new System.Random(seed);

        GenerateRooms(ref dungeon, ref generationRules);
        ConnectRooms(ref dungeon, ref generationRules);

        if (showLogs) print($"Total cells {dungeon.Tiles.Count}"); 

        return dungeon;
    }

    private void GenerateRooms(ref Dungeon dungeon, ref MapGeneratorRules generationRules)
    {
        var totalRooms = rnd.Next(generationRules.minRoom, generationRules.maxRoom);
        if (showLogs) print($"Total room generated {totalRooms}");

        var rooms = new List<Vector2Int>();
        for (int i = 0; i < totalRooms; i++)
        {
            var roomRules = generationRules.roomRules[rnd.Next(0, generationRules.roomRules.Count - 1)];

            var roomWidth = rnd.Next(roomRules.minRoomWidth, roomRules.maxRoomWidth);
            var roomLength = rnd.Next(roomRules.minRoomLength, roomRules.maxRoomLength);
            if (showLogs) print($"room {i} width {roomWidth} room length {roomLength}");

            var spaceBetweenRoom = rnd.Next(generationRules.minSpaceBetweenRoom, generationRules.maxSpaceBetweenRoom);
            Vector2Int roomCoordonate = i == 0 ? Vector2Int.zero : GetRandomPointWithDistance(ref dungeon, spaceBetweenRoom + (roomWidth + roomLength) / 2);

            Room room = new(roomWidth, roomLength);
            rooms.Add(roomCoordonate);

            for (int height = 0; height < roomLength; height++)
            {
                for (int width = 0; width < roomWidth; width++)
                {
                    room.cells[width + height * roomWidth] = new Cell()
                    {
                        tileType = TileType.Ground,
                        pos = new Vector2Int(roomCoordonate.x + width - roomWidth / 2, roomCoordonate.y + height - roomLength / 2)
                    };
                }
            }

            dungeon.Add(room);
        }
    }

    private Vector2Int GetRandomPointWithDistance(ref Dungeon dungeon, int distance)
    {
        // Choix aléatoire d'un point dans la liste
        var randomPoint = dungeon.Tiles[rnd.Next(0, dungeon.Tiles.Count - 1)];
        Vector2Int newPoint = Vector2Int.RoundToInt(randomPoint.pos + distance * new Vector2((float)((rnd.NextDouble() * 2.0) - 1.0), (float)((rnd.NextDouble() * 2.0) - 1.0)));

        if (dungeon.Tiles.All(cell => Vector2Int.Distance(newPoint, cell.pos) > distance))
            return newPoint;
        else
            return GetRandomPointWithDistance(ref dungeon, distance); // Réessayer avec un nouveau point
    }

    private void ConnectRooms(ref Dungeon dungeon, ref MapGeneratorRules generationRules)
    {
        foreach (Room room in dungeon.Rooms)
        {
            var connectedRoom = dungeon.Rooms.Where(r => !r.Equals(room)).OrderBy(_ => rnd.Next()).First();
            var fromRoom = room.cells.OrderBy(_ => rnd.Next()).First();
            var toRoom = connectedRoom.cells.OrderBy(_ => rnd.Next()).First();

            ConnectRoomToRoom(ref dungeon, fromRoom, toRoom);
        }
    }

    private void ConnectRoomToRoom(ref Dungeon dungeon, Cell roomStartCell, Cell roomEndCell)
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
