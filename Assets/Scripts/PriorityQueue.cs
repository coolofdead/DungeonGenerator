using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PriorityQueue : IEnumerable
{
    public List<PathNode> Nodes { get; protected set; } = new List<PathNode>();

    public bool IsEmpty => Nodes.Count == 0;

    public PathNode this[int key]
    {
        get => Nodes[key];
        set => Nodes[key] = value;
    }

    public PathNode GetBestNode()
    {
        PathNode bestPathNode = Nodes.OrderBy(node => node.PathValue).First();
        Nodes.Remove(bestPathNode);

        return bestPathNode;
    }

    public void AddPathNodeIfNotAlready(PathNode pathNode)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i].Walkable == pathNode.Walkable)
            {
                return;
            }
        }

        Nodes.Add(pathNode);
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var node in Nodes)
            yield return node;
    }
}
