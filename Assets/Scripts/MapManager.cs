using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Config")]
    [SerializeField, RequireInterface(typeof(IDungeonGenerable))] private Object dungeonGenerator;
    public IDungeonGenerable DungeonGenerator => dungeonGenerator as IDungeonGenerable;
    [SerializeField, RequireInterface(typeof(IDungeonRulabe))] private List<Object> dungeonGeneratorRules;
    public IEnumerable<IDungeonRulabe> DungeonGeneratorRules => dungeonGeneratorRules as IEnumerable<IDungeonRulabe>;

    [Header("Visual Dungeon")]
    [SerializeField] private Transform mapAnchor;
    [SerializeField] private Tile groundPrefab;

    public Dungeon Dungeon { get; private set; }

    public void Start()
    {
        // Generate map
        Dungeon = (Dungeon)DungeonGenerator.GenerateDungeon(DungeonGeneratorRules);

        GenerateWorldMap();
    }

    public void GenerateWorldMap()
    {
        foreach (Cell cell in Dungeon)
        {
            var newCell = Instantiate(groundPrefab.gameObject, mapAnchor);
            newCell.transform.position = new Vector3(cell.pos.x, 0, cell.pos.y);
        }
    }
}
