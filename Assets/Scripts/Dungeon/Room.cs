using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    public List<Cell> cells;
    public readonly int width;
    public readonly int length;

    public Room(int width, int length)
    {
        this.width = width;
        this.length = length;
        cells = new List<Cell>(width * length);
    }

    public void AddCells(Vector2Int roomCoordonate)
    {
        for (int h = 0; h < length; h++)
        {
            for (int w = 0; w < width; w++)
            {
                cells.Add(new Cell()
                {
                    tileType = Random.Range(0, 100) > 50 ? TileType.Ground : TileType.Water, // TODO : Remove, used only for testing
                    pos = new Vector2Int(roomCoordonate.x + w - width / 2, roomCoordonate.y + h - length / 2),
                });
            }
        }
    }
}