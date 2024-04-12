using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Data", menuName = "Dungeon/Dungeon Data", order = 1)]
public class DungeonData : ScriptableObject
{
    [Header("Map")]
    // Name, Icon, Avg LV
    public DungeonSizeType SizeType;
    public int TotalFloor;

    [field:Header("Room")]
    [field:SerializeField] public DungeonRoomData DungeonRoomData { get; protected set; }

    [field:Header("Corridor")]
    [field: SerializeField] public DungeonCorridorData DungeonCorridorData { get; protected set; }

    [field:Header("Items")]
    [field: SerializeField] public DungeonItemData DungeonItemData { get; protected set; }

    [Header("Mobs")]

    [Header("Floors")]

    [Header("Tiles")]
    public int lol;
}
