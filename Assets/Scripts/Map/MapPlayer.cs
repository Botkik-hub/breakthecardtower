using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Player that store node path
/// </summary>
public class MapPlayer : MonoBehaviour
{
    private byte _currentNode;
    private int _seed;
    private readonly List<byte> _historyNodes = new List<byte>();
    private List<byte> _cashedHistoryNodes;
    
    public byte GetCurrentNode()
    {
        return _currentNode;
    }

    public void SetCurrentNode(byte newNodeNumber)
    {
        _currentNode = newNodeNumber;
    }
    
    public void RestorePosition(byte newNodeNumber)
    {
        _currentNode = newNodeNumber;
    }

    public void RestoreHistory(byte[] nodesNumbers)
    {
        _cashedHistoryNodes = nodesNumbers.ToList();
    }

    public void PlaceOnMap()
    {
        // restore position
        var nodes = FindObjectsOfType<MapNode>();
        if (_currentNode != 0)
        {
            foreach (var mapNode in nodes)
            {
                if (mapNode.NodeNumber == _currentNode)
                {
                    transform.position = mapNode.transform.position + new Vector3(0, 0, -1);
                    mapNode.SwitchNodesWithoutEnter(this);
                    break;
                }
            }
        }

        // restore history
        // TODO optimize this
        _historyNodes.Clear();
        var nodeList = _cashedHistoryNodes.ToList();
        var amountNodesLeft = nodeList.Count;

        if (amountNodesLeft != 0)
        {
            foreach (var mapNode in nodes)
            {
                var number = mapNode.NodeNumber;
                var index = nodeList.IndexOf(number);
                if (index != -1)
                {
                    mapNode.AddToPath();
                }
            }
        }
    }

    public void AddToHistory(byte nodeNumber)
    {
        if (_historyNodes.IndexOf(nodeNumber) == -1)
            _historyNodes.Add(nodeNumber);
    }

    public byte[] GetHistory()
    {
        return _historyNodes.ToArray();
    }

    public int GetSeed()
    {
        return _seed;
    }

    public void SetSeed(int seed)
    {
        _seed = seed;
        
    }
}   
