using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDungeon : IEnumerable
{
    IEnumerable<ICellable> GetCells();
    void AddCells(IEnumerable<ICellable> cells);
    Vector2Int GetSize();
}
