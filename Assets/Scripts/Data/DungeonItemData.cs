using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Item Data", menuName = "Dungeon/Dungeon Item Data", order = 4)]
public class DungeonItemData : ScriptableObject
{
    [Serializable]
    public struct ItemDropData
    {
        public ItemType item;
        [Range(0, 100)] public int dropRate;
    }

    public Range TotalItemToDrop;
    public List<ItemDropData> ItemDrops;
}

[Serializable]
public struct Range
{
    public int min;
    public int max;

    public readonly bool InRange(int value)
    {
        return value >= min && value <= max;
    }
}