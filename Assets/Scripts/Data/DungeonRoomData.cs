using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Room Data", menuName = "Dungeon/Dungeon Room Data", order = 2)]
public class DungeonRoomData : ScriptableObject
{
    public int minRoom = 3;
    public int maxRoom = 5;

    public int minSpaceBetweenRoom = 3;
    public int maxSpaceBetweenRoom = 3;

    public int minRoomWidth = 3;
    public int maxRoomWidth = 5;

    public int minRoomLength = 3;
    public int maxRoomLength = 5;

    // TODO : Add dummies (1x1) room
    public int dummiesToMake = 0;
}
