using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEntity : MonoBehaviour, IMovableAlongPath, ICarrier
{
    public TileType TileWalkable;

    public Inventory inventory;

    public bool CanWalkOn(IWalkable walkable)
    {
        return TileWalkable.HasFlag(walkable.GetWalkableType());
    }

    public void Carry(ICarriable carriable)
    {
        inventory.Add(carriable);
    }

    public bool HasReachPos()
    {
        return true;
    }

    public void Move(Vector3 movePos)
    {
        //print("Move to " + movePos);
        transform.position = movePos + Vector3.up;
    }
}
