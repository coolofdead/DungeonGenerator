using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Dictionary<ItemType, int>
{
    public void Add(ICarriable carriable, int quantity = 1)
    {
        if (ContainsKey(carriable.GetCarryType())) Add(carriable.GetCarryType(), 0);
        this[carriable.GetCarryType()] += quantity;
    }

    public int Get(ICarriable carriable)
    {
        return Get(carriable.GetCarryType());
    }

    public int Get(ItemType item)
    {
        return ContainsKey(item) ? this[item] : 0;
    }
}