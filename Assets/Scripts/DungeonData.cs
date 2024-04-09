using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Data", menuName = "Dungeon/Dungeon Data", order = 1)]
public class DungeonData : ScriptableObject
{
    [Header("Map")]
    // Name
    // Icon
    // Avg LV
    public DungeonSizeType SizeType;
    //public Vector2Int MapSize => SizeType == MapSizeType.Small ? Vector2Int.one * MAP_SIZE_SMALL : SizeType == MapSizeType.Medium ? Vector2Int.one * MAP_SIZE_MEDIUM : Vector2Int.one * MAP_SIZE_LARGE;

    [field:Header("Room")]
    // SO Rooms info (Enumerable)
    [field:SerializeField] public DungeonRoomData DungeonRoomData { get; protected set; }

    [Header("Corridor")]
    // SO Corridor
    //public int nbCorridorCorner = 1;

    [Header("Items")]

    [Header("Mobs")]

    [Header("Floors")]

    [Header("Tiles")]
    public int lol;
}
