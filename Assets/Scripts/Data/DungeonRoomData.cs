using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Room Data", menuName = "Dungeon/Dungeon Room Data", order = 2)]
public class DungeonRoomData : ScriptableObject
{
    [Header("Total Room")]
    public int minRoom = 3;
    public int maxRoom = 5;

    [Header("Space Between Rooms")]
    public int minSpaceBetweenRoom = 3;
    public int maxSpaceBetweenRoom = 3;

    [Header("Room Size")]
    public int minRoomWidth = 3;
    public int maxRoomWidth = 5;

    public int minRoomLength = 3;
    public int maxRoomLength = 5;

    // TODO : Add dummies (1x1) room
    [Header("Dummy")]
    public int dummiesToMake = 0;
}
