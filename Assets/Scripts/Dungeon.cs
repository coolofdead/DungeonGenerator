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

    public Dungeon(DungeonSizeType sizeType)
    {
        MapSize = Vector2Int.one * (int)sizeType;
        Cells = new();
        Rooms = new();
    }

    public Cell this[int index] { get => Cells[index]; set => Cells[index] = value; }

    public void AddCells(IEnumerable<ICellable> cells)
    {
        foreach (var cell in cells)
        {
            Add(cell);
        }
    }

    public void Add(ICellable newCell)
    {
        if (Cells.Any(cell => cell.GetDungeonPosition().x == newCell.GetDungeonPosition().x && cell.GetDungeonPosition().y == newCell.GetDungeonPosition().y))
        {
            Debug.Log($"Try to add duplicate cell at position {newCell.GetDungeonPosition()}");
            return;
        }

        Cells.Add((Cell)newCell);
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
        return Cells.Any(cell => cell.GetDungeonPosition().x == x && cell.GetDungeonPosition().y == y);
    }

    public ICellable TryGet(int x, int y)
    {
        return Cells.Where(cell => cell.GetDungeonPosition().x == x && cell.GetDungeonPosition().y == y).First();
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
    private IEnumerable<IWalkable> Neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => Neighbours;
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => Neighbours = neighbours;

    public Vector2Int GetDungeonPosition() => pos;
    public Vector3 GetPosition() => GetWorldPosition();
    public Vector3 GetWorldPosition() => new Vector3(pos.x, 0, pos.y);
}

public class Room
{
    public List<Cell> cells;
    public readonly int width;
    public readonly int length;

    public Room(int width, int length)
    {
        this.width = width;
        this.length = length;
        cells = Enumerable.Repeat(new Cell(), width * length).ToList();
    }

    public void AddCells(Vector2Int roomCoordonate)
    {
        for (int h = 0; h < length; h++)
        {
            for (int w = 0; w < width; w++)
            {
                cells[w + h * width] = new Cell()
                {
                    tileType = TileType.Ground,
                    pos = new Vector2Int(roomCoordonate.x + w - width / 2, roomCoordonate.y + h - length / 2),
                };
            }
        }
    }
}

public enum TileType
{
    None,
    Wall,
    Ground,
}
