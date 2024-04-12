using System.Collections.Generic;
using UnityEngine;

public interface IWalkable
{
    Vector3 GetPosition();
    TileType GetWalkableType();
    IEnumerable<IWalkable> GetNeighbours();
    bool CanBeWalkedOn();
    void OnMovableWalkOn(IMovableAlongPath movable);
    void OnMovableWalkOff(IMovableAlongPath movable);
}
