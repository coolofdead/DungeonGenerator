using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDungeonGenerable
{
    IDungeon GenerateDungeon(IEnumerable<IDungeonRulabe> rules = null);
}

public interface IDungeonRulabe
{
    void ApplyRuleToDungeon(ref IDungeon dungeon);
}



public interface IDungeon : IEnumerable
{
    IEnumerable<ICellable> GetCells();
    Vector2Int GetSize();
}

public interface ICellable
{
    public Vector2Int GetDungeonPosition();
}

