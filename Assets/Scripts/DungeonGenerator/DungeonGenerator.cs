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
    public DungeonItemGenerator dungeonItemGenerator;
    
    private System.Random rnd;

    private void Awake()
    {
        int seed = this.seed == 0 ? Random.Range(0, 99999) : this.seed;
        rnd = new System.Random(seed);
    }

    public IDungeon GenerateDungeon(DungeonData dungeonData, int floor)
    {
        var dungeon = new Dungeon(dungeonData.SizeType);

        roomGenerator.GenerateRooms(dungeon, dungeonData.DungeonRoomData(floor), rnd);
        corridorGenerator.ConnectRooms(dungeon, dungeonData.DungeonCorridorData(floor), rnd);
        // generate environement (water, lava, air..) cells
        // add dead ends
        dungeonWallGenerator.GenerateWalls(dungeon);
        dungeonStairGenerator.GenerateStair(dungeon, rnd);
        neighboursGenerator.SetTilesNeighbours(dungeon);
        dungeonItemGenerator.DropItems(dungeon, dungeonData.DungeonItemData(floor), rnd);
        // find most top left and bottom right and fill with empty walls cells
        // generate mobs
        // ... add more generation layouts

        if (showLogs) print($"Total cells {dungeon.GetCells().Count()}");

        return dungeon;
    }
}
