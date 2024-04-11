using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// BETTER TO USE ASYNC / AWAIT + COROUTINE FOR MAKING PATH AND USING MORE REF AND STRUCT TO REDUCE MEMORY COST

public class Pathfinder : MonoBehaviour
{
    private PriorityQueue priorityQueue;
    private HashSet<IWalkable> visitedTiles = new();

    [SerializeField] private bool showGlobalDebug = false;
    [SerializeField] private bool showCurrentNode = false;
    [SerializeField] private bool showQueue = false;
    [SerializeField] private bool showVisited = false;

    public IEnumerable<IWalkable> FindPath(IMovableAlongPath movable, IWalkable start, IWalkable target)
    {
        priorityQueue = new PriorityQueue();
        visitedTiles.Clear();

        SetFirstNode(start);
        PathNode pathNode = FindPath(target, movable);

        return FormatPath(pathNode);
    }

    private PathNode FindPath(IWalkable endNode, IMovableAlongPath movable)
    {
        while (!priorityQueue.IsEmpty)
        {
            PathNode currentNode = priorityQueue.GetBestNode();

            if (currentNode.Walkable == endNode)
            {
                if (showGlobalDebug) Debug.Log("Reach end node");
                return currentNode;
            }

            if (showCurrentNode) Debug.Log("Current node is " + currentNode.Walkable);

            visitedTiles.Add(currentNode.Walkable);

            foreach (PathNode pathNode in currentNode.GetTileNeighbours(movable))
            {
                if (!visitedTiles.Contains(pathNode.Walkable))
                {
                    priorityQueue.AddPathNode(pathNode);
                }
            }

            if (showVisited) Debug.Log("Visited " + visitedTiles.Count);
            if (showQueue) Debug.Log("Queue " + priorityQueue.Count);
        }

        return null; // No path found
    }

    private IEnumerable<IWalkable> FormatPath(PathNode pathNode)
    {
        var path = new List<IWalkable>();

        if (pathNode is null) return path;

        while (pathNode.PreviousNode != null)
        {
            path.Add(pathNode.Walkable);
            pathNode = pathNode.PreviousNode;
        }

        path.Reverse();

        return path;
    }

    public void SetFirstNode(IWalkable start)
    {
        var startNode = new PathNode(start);
        priorityQueue.AddPathNode(startNode);
    }
}
