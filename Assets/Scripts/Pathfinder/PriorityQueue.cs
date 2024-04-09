using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PriorityQueue : IEnumerable
{
    public List<PathNode> Nodes { get; private set; } = new List<PathNode>();

    public int Count => Nodes.Count;
    public bool IsEmpty => Nodes.Count == 0;

    public void AddPathNode(PathNode pathNode)
    {
        Nodes.Add(pathNode);
        HeapifyUp(Nodes.Count - 1);
    }

    public PathNode GetBestNode()
    {
        if (IsEmpty)
            throw new InvalidOperationException("PriorityQueue is empty");

        PathNode bestNode = Nodes[0];
        Nodes[0] = Nodes[^1];
        Nodes.RemoveAt(Nodes.Count - 1);
        if (Nodes.Count > 1)
            HeapifyDown(0);
        return bestNode;
    }

    private void HeapifyUp(int index)
    {
        int parentIndex = (index - 1) / 2;
        while (index > 0 && Nodes[index].PathValue < Nodes[parentIndex].PathValue)
        {
            SwapNodes(index, parentIndex);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        int smallest = index;

        if (leftChildIndex < Nodes.Count && Nodes[leftChildIndex].PathValue < Nodes[smallest].PathValue)
            smallest = leftChildIndex;

        if (rightChildIndex < Nodes.Count && Nodes[rightChildIndex].PathValue < Nodes[smallest].PathValue)
            smallest = rightChildIndex;

        if (smallest != index)
        {
            SwapNodes(index, smallest);
            HeapifyDown(smallest);
        }
    }

    private void SwapNodes(int i, int j)
    {
        PathNode temp = Nodes[i];
        Nodes[i] = Nodes[j];
        Nodes[j] = temp;
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var node in Nodes)
            yield return node;
    }
}
