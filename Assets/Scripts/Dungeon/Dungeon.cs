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

    public Cell this[int index] { get => Cells[index]; }
    public Cell this[int x, int y] { get => Cells[x + y * MapSize.y]; }

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
            //Debug.Log($"Try to add duplicate cell at position {newCell.GetDungeonPosition()}");
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

    public bool HasAt(Vector2Int pos)
    {
        return HasAt(pos.x, pos.y);
    }

    public bool HasAt(int x, int y)
    {
        return Cells.Any(cell => cell.GetDungeonPosition().x == x && cell.GetDungeonPosition().y == y);
    }

    public ICellable GetAtPos(int x, int y)
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
