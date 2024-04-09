using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon : IDungeon
{
    public List<Cell> Cells { get; protected set; } = new();
    public List<Room> Rooms { get; protected set; } = new();
    public Vector2Int MapSize { get; protected set; }

    public IEnumerable<ICellable> GetCells() => Cells;
    public Vector2Int GetSize() => MapSize;

    public Dungeon(Vector2Int mapSize)
    {
        MapSize = mapSize;
        Cells = new();
        Rooms = new();
    }

    public Cell this[int index] { get => Cells[index]; set => Cells[index] = value; }

    public void Add(Cell newCell)
    {
        if (!Cells.Any(cell => cell.pos.x == newCell.pos.x && cell.pos.y == newCell.pos.y)) Cells.Add(newCell);
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
        return Cells.Any(cell => cell.pos.x == x && cell.pos.y == y);
    }

    public Cell TryGet(int x, int y)
    {
        return Cells.Where(cell => cell.pos.x == x && cell.pos.y == y).First();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var cell in Cells)
        {
            yield return cell;
        }
    }
}

public class Cell : ICellable, IWalkable
{
    public TileType tileType;
    public Vector2Int pos;
    public IEnumerable<IWalkable> Neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => Neighbours;
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => Neighbours = neighbours;

    public Vector2Int GetDungeonPosition() => pos;
    public Vector3 GetPosition() => GetWorldPosition();
    public Vector3 GetWorldPosition() => new Vector3(pos.x, 0, pos.y);
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
