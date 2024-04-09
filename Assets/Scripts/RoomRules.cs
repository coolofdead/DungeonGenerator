using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Data", menuName = "Dungeon/Room Data", order = 2)]
public class RoomRules : ScriptableObject, IDungeonRulabe
{
    public int minRoomWidth = 3;
    public int maxRoomWidth = 5;

    public int minRoomLength = 3;
    public int maxRoomLength = 5;

    public void ApplyRuleToDungeon(ref IDungeon dungeon)
    {
    }
}
