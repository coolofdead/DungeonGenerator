using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DungeonGenerator : MonoBehaviour, IDungeonGenerable
{
    public bool showLogs = false;
    public int seed;

    public DungeonRoomGenerator roomGenerator;
    public DungeonCorridorGenerator corridorGenerator;
    public DungeonNeighboursGenerator neighboursGenerator;

    private System.Random rnd;

    public IDungeon GenerateDungeon(DungeonData dungeonData)
    {
        var dungeon = new Dungeon(dungeonData.SizeType);

        int seed = this.seed == 0 ? UnityEngine.Random.Range(0, 9999) : this.seed;
        rnd = new System.Random(seed);

        // Other idea to apply rules based on DungeonSO 
        //foreach (var rule in rules)
        //{
        //    rule.ApplyRuleToDungeon(ref dungeon);
        //}

        roomGenerator.GenerateRooms(dungeon, dungeonData, rnd);
        corridorGenerator.ConnectRooms(dungeon, dungeonData, rnd);
        neighboursGenerator.SetTilesNeighbours(dungeon);
        // ... add more generation layouts

        if (showLogs) print($"Total cells {dungeon.GetCells().Count()}");

        return dungeon;
    }
}
