using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDungeonGenerable
{
    IDungeon GenerateDungeon(DungeonData dungeonData, int floor); // Super wrong
}

