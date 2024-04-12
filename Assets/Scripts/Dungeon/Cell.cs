using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ICellable, IWalkable
{
    public readonly TileType tileType;
    public readonly Vector2Int pos;
    public ICarriable carriable;
    public bool HasStair;

    public IMovableAlongPath WalkbaleOnCell { get; private set; }
    private IEnumerable<IWalkable> Neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => Neighbours;
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => Neighbours = neighbours;

    public Vector2Int GetDungeonPosition() => pos;
    public Vector3 GetPosition() => GetWorldPosition();
    public Vector3 GetWorldPosition() => new Vector3(pos.x, 0, pos.y);

    public Cell(Vector2Int pos, TileType tileType, ICarriable carriable = null)
    {
        this.pos = pos;
        this.tileType = tileType;
    }

    public bool CanBeWalkedOn()
    {
        return WalkbaleOnCell == null;
    }

    public void OnMovableWalkOn(IMovableAlongPath movable) 
    {
        WalkbaleOnCell = movable;

        if (carriable != null && movable is ICarrier)
        {
            (movable as ICarrier)?.Carry(carriable);
            carriable = null;
        }
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

[Flags]
public enum TileType
{
    None = 0,
    Wall = 1,
    Ground = 2,
    Lava = 2,
    Water = 4,
    Air = 8,
}
