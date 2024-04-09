using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Data", menuName = "Dungeon/Dungeon Data", order = 1)]
public class MapGeneratorRules : ScriptableObject, IDungeonRulabe
{
    public const int MAP_SIZE_LARGE = 30;
    public const int MAP_SIZE_MEDIUM = 15;
    public const int MAP_SIZE_SMALL = 8;

    [Header("Map")]
    public MapSizeType SizeType;
    public Vector2Int MapSize => SizeType == MapSizeType.Small ? Vector2Int.one * MAP_SIZE_SMALL : SizeType == MapSizeType.Medium ? Vector2Int.one * MAP_SIZE_MEDIUM : Vector2Int.one * MAP_SIZE_LARGE;

    [Header("Room")]
    public int minRoom = 3;
    public int maxRoom = 5;

    public int minSpaceBetweenRoom = 3;
    public int maxSpaceBetweenRoom = 3;
    public List<RoomRules> roomRules;

    [Header("Corridor")]
    public int nbCorridorCorner = 1;

    public void ApplyRuleToDungeon(ref IDungeon dungeon)
    {
    }
}

public enum MapSizeType
{
    Small,
    Medium,
    Large,
}