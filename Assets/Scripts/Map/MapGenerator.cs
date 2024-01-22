using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Generates map
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapConfig mapConfig;
    [SerializeField] private GameObject nodePrefab;
    
     
    private readonly List<List<MapNode>> _rows = new List<List<MapNode>>();
    private readonly List<float> _rowPositions = new List<float>();

    private int _seed;

    private float _rowHeight;
    private int _rowAmount;

    private float _bottomBorder;
    private float _topBorder;
    
    private float _leftBorder;
    private float _rightBorder;

    private float _rowDistance;
    private float _nodeDistance;
    
    private float _rowHeightWithoutSpace;

    private void Start()
    {
        GenerateMap();
    }
    
    /// <summary>
    /// General rules for map generation:
    /// 1. max 2 chests per floor
    /// 2. 3 fixed rows for 4 nodes
    /// 3. middle and end rows should be non combat
    /// 4. 2 or 3 nodes between fixed rows
    /// 5. at least one fight node in the half of the map
    /// 
    /// 6. No cross connections?
    /// 7. 4 nodes in a row (for now)
    /// 8. Min distance between nodes? 
    /// </summary>
    private void GenerateMap()
    {
        ReadData();
        Random.InitState(_seed);

        //Structure for now

        // 1.Place Rows 
        PlaceRows();
        // 2.Create nodes & assign its types 
        CreateNodes();
        // 3.Connect nodes
        ConnectNodes();
        // 4.Break cross connections
        BreakCrossConnections();
        // 5. Activate start nodes
        ActivateStartNodes();
        

        // 6. Place player
        PlacePlayer();
    }

    private void PlacePlayer()
    {
        var player = FindObjectOfType<MapPlayer>();
        player.PlaceOnMap();
    }


    private void ReadData()
    {
        // Get Seed here from player
        //_seed = 0;
        var player = FindObjectOfType<MapPlayer>();
        _seed = player.GetSeed();
        //_seed = (int)(Random.value * int.MaxValue);

        _rowDistance = mapConfig.distanceBetweenLayers;
        _nodeDistance = mapConfig.distanceBetweenNodes;
        
        _bottomBorder = mapConfig.bottomBorder;
        _topBorder = mapConfig.topBorder;
        _leftBorder = mapConfig.leftBorder;
        _rightBorder = mapConfig.rightBorder;

        _rowAmount = mapConfig.Layers.Count; 
        _rowHeightWithoutSpace = (_topBorder - _bottomBorder) /  _rowAmount - _rowDistance;
        _rowHeight = _rowHeightWithoutSpace + _rowDistance;

    }

    private void PlaceRows()
    {
        var halfRowSize = _rowHeightWithoutSpace / 2;
        for (int i = 0; i < _rowAmount; i++)
        {
            _rowPositions.Add(_rowHeight * i + halfRowSize);
        }
    }

    private void CreateNodes()
    {
        var halfRowSize = _rowHeightWithoutSpace / 2;
        var rowNumber = 0;
        
        var rowWidth = _rightBorder - _leftBorder;
        
        foreach (var rowPosition in _rowPositions)
        {
            var rowStart = rowPosition - halfRowSize;
            var rowEnd = rowPosition + halfRowSize;

            var nodesInRow = 4;
            var nodeWidth = rowWidth / nodesInRow - _nodeDistance;
            var nodeWidthWithSpace = nodeWidth + _nodeDistance;
            
            _rows.Add(new List<MapNode>());
            for (var i = 0; i < nodesInRow; i++)
            {
                var leftPlacementBorder = _leftBorder + nodeWidthWithSpace * i;
                var rightPlacementBorder = leftPlacementBorder + nodeWidth;

                //print($"Row {rowNumber}, Node {i}, minX {leftPlacementBorder}, maxX {rightPlacementBorder}, minY {rowStart}, maxY {rowEnd}");
                
                var x = Random.Range(leftPlacementBorder, rightPlacementBorder);
                var y = Random.Range(rowStart, rowEnd);

                _rows[rowNumber].Add(Instantiate(nodePrefab).GetComponent<MapNode>());
                _rows[rowNumber][i].transform.position = new Vector3(x, y, 0.0f);
                _rows[rowNumber][i].SetUp();
                AssignType(rowNumber, i);
            }
            rowNumber++;
        }
    }

    private void AssignType(int row, int nodeNumber)
    {
        // TODO add checks for combat nodes in chain
        
        var layer = mapConfig.Layers[row];
        var chances = layer.nodes;
        var number = Random.value;

        var currentStartRange = 0.0f;
        
        foreach (var chance in chances)
        {
            if (number > currentStartRange && number < currentStartRange + chance.chance)
            {
                _rows[row][nodeNumber].SetType(chance.nodeType);
                return;
            }

            currentStartRange += chance.chance;
        }
        // If node type not assigned, config does not add up to 1

        throw new InvalidDataException($"Chances in row {row} not sum up to 1");
    }

    private void ConnectNodes()
    {
        // each row beside last one
        var iterateAmount = _rows.Count - 1;
        for (int rowIndex = 0; rowIndex < iterateAmount; rowIndex++)
        {
            var row = _rows[rowIndex];
            var nextRow = _rows[rowIndex + 1];
            
            var rowLength = row.Count;

            for (int nodeIndex = 0; nodeIndex < rowLength; nodeIndex++)
            {
                var node = row[nodeIndex];
                var nextRowNode = nextRow[nodeIndex];

                node.ConnectToNode(nextRowNode);

                const float chanceToConnectSides = 0.5f;

                var randNumLeft = Random.value;
                var randNumRight = Random.value;

                if (nodeIndex != 0)
                {
                    if (randNumLeft > chanceToConnectSides)
                    {
                        var leftNextRow = nextRow[nodeIndex - 1];
                        node.ConnectToNode(leftNextRow);
                    }
                }

                if (nodeIndex != rowLength - 1)
                {
                    if (randNumRight > chanceToConnectSides)
                    {
                        var rightNextRow = nextRow[nodeIndex + 1];
                        node.ConnectToNode(rightNextRow);
                    }
                }
            }
        }
    }

    private void ActivateStartNodes()
    {
        foreach (var startNode in _rows[0])
        {
            startNode.SetActive(true);
        }
    }

    private void BreakCrossConnections()
    {
        // All rows beside the last one
        var rowAmount = _rows.Count - 1; 
        for (int rowIndex = 0; rowIndex < rowAmount; rowIndex++)
        {
            var row = _rows[rowIndex];
            var nextRow = _rows[rowIndex + 1];
            
            // all nodes beside the last one
            var nodeAmount = row.Count - 1;

            for (int nodeIndex = 0; nodeIndex < nodeAmount; nodeIndex++)
            {
                // Check connection to the right
                var node = row[nodeIndex];
                var nextNode = nextRow[nodeIndex];

                var rightNode = row[nodeIndex + 1];
                var nextRightNode = nextRow[nodeIndex + 1];
                
                // continue if no cross connection
                if (!node.IsConnectedTo(nextRightNode)) continue;
                if (!rightNode.IsConnectedTo(nextNode)) continue;

                var destroyNumber = Random.value;
                if (destroyNumber < 0.5f)
                {
                    //destroy right facing connection
                    node.DisconnectNode(nextRightNode);
                }
                else
                {
                    //destroy left facing connection
                    rightNode.DisconnectNode(nextNode);
                }
            }
        }
    }
    
}
