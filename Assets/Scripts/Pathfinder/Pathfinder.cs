using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public delegate void OnPathUpdate(IWalkable currentWalkable, List<IWalkable> visitedWalkables, List<PathNode> walkablesToVisit);
    public OnPathUpdate onPathUpdate;

    private PriorityQueue priorityQueue;
    private List<IWalkable> visitedTile = new();

    [SerializeField] private bool showGlobalDebug = false;
    [SerializeField] private bool showCurrentNode = false;
    [SerializeField] private bool showQueue = false;
    [SerializeField] private bool showVisited = false;

    public IEnumerable<IWalkable> FindPath(IWalkable start, IWalkable target)
    {
        priorityQueue = new PriorityQueue();
        visitedTile.Clear();

        SetFirstNode(start);
        PathNode pathNode = FindPath(target);

        return FormatPath(pathNode);
    }

    private PathNode FindPath(IWalkable endNode, PathNode optimalNode = null)
    {
        if (optimalNode?.Walkable == endNode || priorityQueue.IsEmpty)
        {
            return optimalNode;
        }

        PathNode currentNode = priorityQueue.GetBestNode();
        if (showCurrentNode) print("Current node is " + currentNode.Walkable);

        if (currentNode.Walkable != endNode)
        {
            visitedTile.Add(currentNode.Walkable);

            if (showVisited) print("Visited " + visitedTile.Count);

            foreach (PathNode pathNode in currentNode.GetTileNeighbours(currentNode))
            {
                if (!visitedTile.Contains(pathNode.Walkable)) priorityQueue.AddPathNodeIfNotAlready(pathNode); ;
            }

            if (showQueue) print("Queue " + priorityQueue.Nodes.Count);

            onPathUpdate?.Invoke(currentNode.Walkable, visitedTile, priorityQueue.Nodes);

            optimalNode = FindPath(endNode, optimalNode);

            return optimalNode;
        }
        else
        {
            if (showGlobalDebug) print("Reach end node");
            return currentNode;
        }
    }

    private IEnumerable<IWalkable> FormatPath(PathNode pathNode)
    {
        // Loop from target to start and store it inside a list
        var path = new List<IWalkable>();

        if (pathNode == null) return path;

        while (pathNode.PreviousNode != null)
        {
            path.Add(pathNode.Walkable);

            pathNode = pathNode.PreviousNode;
        }

        // Reverse it to have the start at the begining of the list
        path.Reverse();

        return path;
    }

    public void SetFirstNode(IWalkable start)
    {
        var startNode = new PathNode(start);
        priorityQueue.AddPathNodeIfNotAlready(startNode);
    }
}
