using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Data", menuName = "Dungeon/Dungeon Data", order = 1)]
public class MapGeneratorRules : ScriptableObject
{
    [Header("Map")]
    public Vector2Int MapSize;
    public int seed;

    [Header("Room")]
    public int minRoom = 3;
    public int maxRoom = 5;

    public int minSpaceBetweenRoom = 3;
    public int maxSpaceBetweenRoom = 3;
    public List<RoomRules> roomRules;


    [Header("Corridor")]
    public int nbCorridorCorner = 1;
}
