using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonItemGenerator : MonoBehaviour
{
    public void DropItems(Dungeon dungeon, DungeonItemData itemData, System.Random rnd)
    {
        var nbItems = rnd.Next(itemData.TotalItemToDrop.min, itemData.TotalItemToDrop.max);

        for (int i = 0; i < nbItems; i++)
        {
            var itemToDrop = itemData.ItemDrops[rnd.Next(0, itemData.ItemDrops.Count)];

            var room = dungeon.Rooms.OrderBy(_ => rnd.Next()).First();
            var cell = room.cells.Where(cell => !cell.isImmutable && cell.carriable == null).OrderBy(_ => rnd.Next()).First();
            cell.carriable = ResourcePooler.Instance.Get<ICarriable>(itemToDrop.item);
        }
    }
}
