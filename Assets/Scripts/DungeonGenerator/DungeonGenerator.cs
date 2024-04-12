using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonGenerator : MonoBehaviour, IDungeonGenerable
{
    public bool showLogs = false;
    public int seed;

    public DungeonRoomGenerator roomGenerator;
    public DungeonCorridorGenerator corridorGenerator;
    public DungeonNeighboursGenerator neighboursGenerator;
    public DungeonWallGenerator dungeonWallGenerator;
    public DungeonStairGenerator dungeonStairGenerator;

    private System.Random rnd;

    public IDungeon GenerateDungeon(DungeonData dungeonData)
    {
        var dungeon = new Dungeon(dungeonData.SizeType);

        int seed = this.seed == 0 ? UnityEngine.Random.Range(0, 9999) : this.seed;
        rnd = new System.Random(seed);

        // Other idea to apply rules based on DungeonSO 
        //foreach (var rule in rules)
        //{
        //    rule.ApplyRuleToDungeon(dungeon);
        //}

        roomGenerator.GenerateRooms(dungeon, dungeonData, rnd);
        corridorGenerator.ConnectRooms(dungeon, dungeonData, rnd);
        dungeonWallGenerator.GenerateWalls(dungeon);
        dungeonStairGenerator.GenerateStair(dungeon, rnd);
        neighboursGenerator.SetTilesNeighbours(dungeon);
        // generate items
        // generate mobs
        // generate environement (water, lava, air..) cellsx
        // find most top left and bottom right and fill with empty walls cells
        // ... add more generation layouts

        if (showLogs) print($"Total cells {dungeon.GetCells().Count()}");

        return dungeon;
    }
}
