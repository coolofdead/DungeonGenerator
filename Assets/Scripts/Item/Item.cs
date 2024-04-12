using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Item : MonoBehaviour, ICarriable
{
    public abstract ItemType ItemType { get; }

    public int CountInactive => throw new System.NotImplementedException();

    public abstract void Use();

    public ItemType GetCarryType()
    {
        return ItemType;
    }
}

