using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Dungeon Debug")]
    public DungeonData DungeonData;

    [Header("Map Config")]
    [SerializeField, RequireInterface(typeof(IDungeonGenerable))] private Object dungeonGenerator;
    public IDungeonGenerable DungeonGenerator => dungeonGenerator as IDungeonGenerable;

    [Header("Visual Dungeon")]
    [SerializeField] private Transform mapAnchor;
    [SerializeField] private Tile groundPrefab;
    [SerializeField] private Tile waterPrefab;
    [SerializeField] private Tile wallPrefab;
    [SerializeField] private Tile stairPrefab;

    public Dungeon Dungeon { get; private set; }

    public void Start()
    {
        // Generate map
        Dungeon = (Dungeon)DungeonGenerator.GenerateDungeon(DungeonData);

        GenerateWorldMap();
    }

    public void GenerateWorldMap()
    {
        foreach (Cell cell in Dungeon)
        {
            Tile tilePrefab = null;
            switch (cell.GetWalkableType())
            {
                case TileType.Ground:
                    tilePrefab = groundPrefab;
                    break;
                case TileType.Water:
                    tilePrefab = waterPrefab;
                    break;
                case TileType.Wall:
                    tilePrefab = wallPrefab;
                    break;
                default:
                    tilePrefab = groundPrefab;
                    break;
            }
            if (cell.HasStair) tilePrefab = stairPrefab;

            Tile newCell = Instantiate(tilePrefab, mapAnchor);
            newCell.transform.position = new Vector3(cell.pos.x, 0, cell.pos.y);
        }
    }

    public Cell GetCellAt(Transform objectTransform)
    {
        var pos = Vector3Int.FloorToInt(objectTransform.transform.position);
        return (Cell)Dungeon.GetAtPos(pos.x, pos.z);
    }
}
