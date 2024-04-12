using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public DungeonManager dungeonManager;

    [Header("Visual Dungeon")]
    [SerializeField] private Transform mapAnchor;
    [SerializeField] private Tile groundPrefab;
    [SerializeField] private Tile waterPrefab;
    [SerializeField] private Tile wallPrefab;
    [SerializeField] private Tile stairPrefab;

    public void GenerateWorldMap(Dungeon dungeon)
    {
        foreach (Cell cell in dungeon)
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

    public void ClearWorldMap()
    {
        foreach (Transform child in mapAnchor)
        {
            Destroy(child.gameObject);
        }
    }

    public Cell GetCellAt(Transform objectTransform)
    {
        var pos = Vector3Int.FloorToInt(objectTransform.transform.position);
        return (Cell)dungeonManager.Dungeon.GetAtPos(pos.x, pos.z);
    }
}
