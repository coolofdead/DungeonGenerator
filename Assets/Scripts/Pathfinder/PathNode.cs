using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathNode
{
    public readonly IWalkable Walkable;
    public readonly PathNode PreviousNode;

    /// <summary>
    /// Is used to calculate the total tiles used for this tile's path
    /// </summary>
    public int PathValue { get; private set; }

    public PathNode (IWalkable walkable = null, PathNode previousNode = null, int pathValue = 0)
    {
        Walkable = walkable;
        PreviousNode = previousNode;
        PathValue = pathValue;
    }

    public IEnumerable<PathNode> GetTileNeighbours(IMovableAlongPath movable)
    {
        foreach (IWalkable neighbourWalkable in Walkable.GetNeighbours().Where(neighbour => movable.CanWalkOn(neighbour)))
        {
            yield return new PathNode(neighbourWalkable, this, PathValue + 1);
        }
    }

    public float GetTotalCost()
    {
        return PathValue;
    }
}
