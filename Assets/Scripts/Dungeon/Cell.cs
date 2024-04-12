using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface 

public interface ICarriable
{
    void Carry();
}

public class Cell : ICellable, IWalkable
{
    public TileType tileType;
    public ItemType Item;
    public Vector2Int pos;
    public IMovableAlongPath WalkbaleOnCell { get; private set; }
    private IEnumerable<IWalkable> Neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => Neighbours;
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => Neighbours = neighbours;

    public Vector2Int GetDungeonPosition() => pos;
    public Vector3 GetPosition() => GetWorldPosition();
    public Vector3 GetWorldPosition() => new Vector3(pos.x, 0, pos.y);

    public Cell()
    {

    }

    public bool CanBeWalkedOn()
    {
        return WalkbaleOnCell == null;
    }

    public void OnMovableWalkOn(IMovableAlongPath movable) 
    {
        WalkbaleOnCell = movable;
    }

    public void OnMovableWalkOff(IMovableAlongPath movable)
    {
        WalkbaleOnCell = null;
    }

    public TileType GetWalkableType()
    {
        return tileType;
    }
}

[System.Flags]
public enum TileType
{
    None = 0,
    Wall = 1,
    Ground = 2,
    Lava = 2,
    Water = 4,
    Air = 8,
    Stairs = Ground,
}
