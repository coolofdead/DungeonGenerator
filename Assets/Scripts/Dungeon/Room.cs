using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    public List<Cell> cells;
    public readonly int width;
    public readonly int length;
    public readonly bool isDummyRoom;
    public Vector2Int RoomCoordonate { get; private set; }

    public Room(int width, int length, bool isDummyRoom)
    {
        this.width = width;
        this.length = length;
        this.isDummyRoom = isDummyRoom;
        cells = new List<Cell>(width * length);
    }

    public void AddCells(Vector2Int roomCoordonate)
    {
        var isDummyRoom = width * length == 1;
        RoomCoordonate = roomCoordonate;

        for (int h = 0; h < length; h++)
        {
            for (int w = 0; w < width; w++)
            {
                var pos = new Vector2Int(roomCoordonate.x + w - width / 2, roomCoordonate.y + h - length / 2);
                var tileType = TileType.Ground;
                cells.Add(new Cell(pos, tileType, isImmutable: isDummyRoom));
            }
        }
    }

    public IEnumerable<Cell> GetEdges()
    {
        var mostRightPos = RoomCoordonate.x - width / 2;
        var mostLeftPos = RoomCoordonate.x + width / 2;
        var mostTopPos = RoomCoordonate.y - length / 2;
        var mostBottomPos = RoomCoordonate.y + length / 2;

        return cells.Where(cell => cell.GetDungeonPosition().x == mostRightPos || cell.GetDungeonPosition().x == mostLeftPos || cell.GetDungeonPosition().y == mostTopPos || cell.GetDungeonPosition().y == mostBottomPos);
    }
}