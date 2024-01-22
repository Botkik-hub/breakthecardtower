using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Template for map generation rules
/// </summary>
[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public ushort rowSize = 4;
    [Header("Where at the scene node will spawn")]
    [Space]
    public float bottomBorder;
    public float topBorder;
    [Space] 
    public float leftBorder;
    public float rightBorder;
    [Space] 
    [Header("Minimal distance between nodes/layers")]
    public float distanceBetweenNodes;
    public float distanceBetweenLayers;

    [Header("Adjust nodes chances from top to bottom")]
    public List<MapLayer> Layers;

    private void OnValidate()
    {
        foreach (var layer in Layers)
        {
            if (layer.isFixedRow)
            {
                layer.canBeSplit = false;
            }
            
            var usedTypes = new List<ENodeType>();
            var totalChance = 0.0f;
            var lastChance = 1.0f;
            foreach (var node in layer.nodes)
            {
                if (node.nodeType != ENodeType.DefaultNode)
                {
                    if (usedTypes.Contains(node.nodeType)) 
                    {
                        node.nodeType = ENodeType.DefaultNode;
                    }
                    else
                    {
                        usedTypes.Add(node.nodeType);
                    }
                }

                if (node.chance > lastChance)
                {
                    node.chance = lastChance;
                }
                
                if (1.0f - totalChance < node.chance)
                {
                    node.chance = 1.0f - totalChance;
                }

                lastChance = node.chance;
                totalChance += node.chance;
            }
        }
    }
}

[System.Serializable]
public class NodeChance
{
    public ENodeType nodeType = ENodeType.DefaultNode;
    [Range(0.0f, 1.0f)]public float chance = 0.0f;
}

[System.Serializable]
public class MapLayer
{
    public List<NodeChance> nodes = new List<NodeChance>();
    public bool isFixedRow = false;
    public bool canBeSplit = false;
}