using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
    public override ItemType ItemType => ItemType.Gold;

    public override void Use()
    {
        print("Use gold");
    }
}
