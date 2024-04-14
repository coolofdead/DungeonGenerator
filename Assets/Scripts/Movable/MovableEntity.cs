using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MovableEntity : MonoBehaviour, IMovableAlongPath, ICarrier
{
    public const float MOVE_SPEED = 0.12f;

    public TileType TileWalkable;
    public Inventory inventory = new();

    private bool hasReachedPos = false;

    public bool CanCarry(ICarriable carriable)
    {
        return true;
    }

    public bool CanWalkOn(IWalkable walkable)
    {
        return TileWalkable.HasFlag(walkable.GetWalkableType());
    }

    public void Carry(ICarriable carriable)
    {
        inventory.Add(carriable);
        ResourcePooler.Instance[carriable.GetCarryType()].Release((carriable as MonoBehaviour).gameObject);
    }

    public bool HasReachPos()
    {
        return hasReachedPos;
    }

    public void Move(Vector3 movePos)
    {
        hasReachedPos = false;
        transform.DOMove(movePos + Vector3.up, MOVE_SPEED).OnComplete(() => hasReachedPos = true).SetEase(Ease.Linear);
    }
}
