using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : Item
{
    public override ItemType ItemType { get => ItemType.BlueBerry; }

    public override void Use()
    {
        print("Use berry");
    }
}
