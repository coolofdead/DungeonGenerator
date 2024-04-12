using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ICarriable
{
    public abstract ItemType ItemType { get; protected set; }

    public ItemType GetCarryType()
    {
        return ItemType;
    }
}

