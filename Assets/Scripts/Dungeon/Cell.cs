using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ICellable, IWalkable
{
    public TileType tileType;
    public Vector2Int pos;
    [ObsoleteAttribute] public object ObjectOnCell { get; private set; }
    private IEnumerable<IWalkable> Neighbours;

    public IEnumerable<IWalkable> GetNeighbours() => Neighbours;
    public void SetNeighbours(IEnumerable<IWalkable> neighbours) => Neighbours = neighbours;

    public Vector2Int GetDungeonPosition() => pos;
    public Vector3 GetPosition() => GetWorldPosition();
    public Vector3 GetWorldPosition() => new Vector3(pos.x, 0, pos.y);

    public bool CanBeWalkedOn()
    {
        return true;
    }

    public void OnMovableWalkOn(IMovableAlongPath movable) 
    {
        ObjectOnCell = movable;
    }

    public void OnMovableWalkOff(IMovableAlongPath movable)
    {
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
}
