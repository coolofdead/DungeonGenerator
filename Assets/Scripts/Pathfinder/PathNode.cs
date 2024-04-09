using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathNode
{
    public readonly IWalkable Walkable;
    public readonly PathNode PreviousNode;

    /// <summary>
    /// Is used to calculate the total tiles used for this tile's path
    /// </summary>
    public int PathValue { get; private set; }

    public IEnumerable<PathNode> GetTileNeighbours(PathNode previousNode)
    {
        foreach (IWalkable neighbourWalkable in Walkable.GetNeighbours())
        {
            yield return new PathNode(neighbourWalkable, previousNode, previousNode.PathValue + 1);
        }
    }

    public PathNode (IWalkable walkable = null, PathNode previousNode = null, int pathValue = 0)
    {
        Walkable = walkable;
        PreviousNode = previousNode;
        PathValue = pathValue;
    }
}
