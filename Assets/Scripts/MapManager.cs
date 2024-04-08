using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Config")]
    [SerializeField] private Transform mapAnchor;
    [SerializeField] private DungeonGenerator dungeonGenerator;
    [SerializeField] private MapGeneratorRules mapGeneratorRules;

    [Header("Visual Dungeon")]
    [SerializeField] private Tile groundPrefab;

    public Dungeon Dungeon { get; private set; }

    public void Start()
    {
        // Generate map
        Dungeon = dungeonGenerator.GenerateDungeon(mapGeneratorRules);

        GenerateWorldMap();

        // Set Tiles neighbours
        SetTilesNeighbours();
    }

    public void GenerateWorldMap()
    {
        foreach (Cell cell in Dungeon)
        {
            var newCell = Instantiate(groundPrefab.gameObject);
            newCell.transform.position = new Vector3(cell.pos.x, 0, cell.pos.y);
        }
    }

    private void SetTilesNeighbours()
    {
        foreach (Cell cell in Dungeon)
        {
            var neighbours = Dungeon.Tiles.Cast<IWalkable>().Where((walkable) => {
                return (walkable.GetPosition().x == cell.pos.x + 1 && walkable.GetPosition().z == cell.pos.y)
                    || (walkable.GetPosition().x == cell.pos.x - 1 && walkable.GetPosition().z == cell.pos.y)
                    || (walkable.GetPosition().z == cell.pos.y + 1 && walkable.GetPosition().x == cell.pos.x)
                    || (walkable.GetPosition().z == cell.pos.y - 1 && walkable.GetPosition().x == cell.pos.x);
            });

            cell.SetNeighbours(neighbours);
        }
    }
}
