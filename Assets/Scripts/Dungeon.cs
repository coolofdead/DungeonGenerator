using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon : IEnumerable
{
    public List<Cell> Tiles { get; protected set; } = new();
    public List<Room> Rooms { get; protected set; } = new();
    public Vector2Int MapSize { get; private set; }

    public Dungeon(Vector2Int mapSize)
    {
        MapSize = mapSize;
        Tiles = new();
        Rooms = new();
    }

    public Cell this[int index] { get => Tiles[index]; set => Tiles[index] = value; }

    public void Add(Cell newCell)
    {
        if (!Tiles.Any(cell => cell.pos.x == newCell.pos.x && cell.pos.y == newCell.pos.y)) Tiles.Add(newCell);
    }

    public void Add(Room room)
    {
        Rooms.Add(room);
        foreach (var cell in room.cells)
        {
            Add(cell);
        }
    }

    public bool HasAt(int x, int y)
    {
        return Tiles.Any(tile => tile.pos.x == x && tile.pos.y == y);
    }

    public Cell TryGet(int x, int y)
    {
        return Tiles.Where(tile => tile.pos.x == x && tile.pos.y == y).First();
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var tile in Tiles)
        {
            yield return tile;
        }
    }
}

public class Cell : IWalkable
{
    public TileType tileType;
    public Vector2Int pos;
    public IEnumerable<IWalkable> neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => neighbours;
    public Vector3 GetPosition() => new Vector3(pos.x, 0, pos.y);
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => this.neighbours = neighbours;
}

public class Room
{
    public List<Cell> cells;

    public Room(int width, int length)
    {
        cells = Enumerable.Repeat(new Cell(), width * length).ToList();
    }
}

public enum TileType
{
    None,
    Wall,
    Ground,
}
