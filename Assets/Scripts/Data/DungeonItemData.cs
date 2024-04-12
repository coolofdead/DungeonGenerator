using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Item Data", menuName = "Dungeon/Dungeon Item Data", order = 4)]
public class DungeonItemData : ScriptableObject
{
    [Serializable]
    public struct ItemDropData
    {
        public ItemType item;
        public Range<int> floor; // = 1..5
        [Range(0, 100)] public int dropRate;
    }

    public Range<int> TotalItemToDrop;
    public List<ItemDropData> ItemDrops;
}

[Serializable]
public struct Range<T>
{
    public T min;
    public T max;
}