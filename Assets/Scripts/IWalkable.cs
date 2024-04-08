using System.Collections.Generic;
using UnityEngine;

public interface IWalkable
{
    Vector3 GetPosition();
    IEnumerable<IWalkable> GetNeighbours();
}
