using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeon Debug")]
    public DungeonData DebugDungeonData;

    [Header("Dungeon Generator")]
    public DungeonGenerator dungeonGenerator;
    public MapManager mapManager;

    [Header("Player")]
    public MovableEntity player;

    public Dungeon Dungeon { get; private set; }
    public DungeonData DungeonData { get; private set; }
    public int CurrentFloor { get; private set; }

    public void Start()
    {
        EnterDungeon(DebugDungeonData);

        Cell.onMovableWalkOnStair += MoveToNextFloor;
    }

    public void EnterDungeon(DungeonData dungeonData)
    {
        DungeonData = dungeonData;
        CurrentFloor = 1;

        Generate();
    }

    public void MoveToNextFloor(IMovableAlongPath movable)
    {
        if (CurrentFloor == DungeonData.TotalFloor)
        {
            print("Finish dungeon");
            return;
        }

        print($"Move to next floor ({CurrentFloor + 1})");
        CurrentFloor++;

        mapManager.ClearWorldMap();
        Generate();
    }

    public void Generate()
    {
        Dungeon = (Dungeon)dungeonGenerator.GenerateDungeon(DungeonData);
        mapManager.GenerateWorldMap(Dungeon);

        player.transform.position = Vector3.up;
        var posX = Dungeon.GetCells().Sum(cell => cell.GetDungeonPosition().x) / Dungeon.Cells.Count();
        var posY = Dungeon.GetCells().Sum(cell => cell.GetDungeonPosition().y) / Dungeon.Cells.Count();
        Camera.main.transform.position = new Vector3(posX, 20, posY);
        // Todo : place player somewhere
    }

    private void OnDestroy()
    {
        Cell.onMovableWalkOnStair -= MoveToNextFloor;
    }
}
