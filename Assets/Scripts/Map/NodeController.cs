using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles active nodes
/// </summary>
public class NodeController : MonoBehaviour
{
    private readonly List<MapNode> ActiveNodes = new List<MapNode>();

    private byte _nextNodeNumber = 1;

    public void AddActiveNode(MapNode node)
    {
        ActiveNodes.Add(node);
    }

    public void RemoveActiveNode(MapNode node)
    {
        ActiveNodes.Remove(node);
    }

    public void DisableAllNodes()
    {
        while (ActiveNodes.Count != 0)
        {
            ActiveNodes[0].SetActive(false);
        }
    }

    public byte GetNextNodeNumber()
    {
        return _nextNodeNumber++;
    }
}
