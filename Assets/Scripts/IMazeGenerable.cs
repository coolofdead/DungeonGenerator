using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDungeonGenerable
{
    IDungeon GenerateDungeon(DungeonData dungeonData); // Super wrong
}



public interface IDungeon : IEnumerable
{
    IEnumerable<ICellable> GetCells();
    void AddCells(IEnumerable<ICellable> cells);
    Vector2Int GetSize();
}

public interface ICellable
{
    public Vector2Int GetDungeonPosition();
}

