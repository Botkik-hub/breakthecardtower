using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// Handles logic of map node
/// TODO: split visual and logic
/// </summary>
public class MapNode : MonoBehaviour
{
    [FormerlySerializedAs("connected")] [SerializeField]
    private MapNode[] connectedAtStart;

    [SerializeField] private GameObject linePrefab;
    [Space] [SerializeField] private Material enabledMaterial;
    [SerializeField] private Material disabledMaterial;
    [SerializeField] private Material pathMaterial;
    [Space] [SerializeField] private float arrowOffsetRadius = 1.0f;

    [SerializeField] private bool isStartNode;
    
    
    [Space]
    [SerializeField] private SpriteRenderer iconBackground;
    [SerializeField] private SpriteRenderer iconRenderer;
    [SerializeField] private Sprite chestIcon;
    [SerializeField] private Sprite combatIcon;
    [SerializeField] private Sprite crownIcon;


    private bool _inPath;
    private bool _isActive;

    public byte NodeNumber { get; private set; }

    private NodeController _controller;

    private ENodeType _type = ENodeType.DefaultNode;

    private readonly Dictionary<MapNode, LineRenderer> _connectedNodes = new Dictionary<MapNode, LineRenderer>();

    private bool _setUpped = false;
    
    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        if (_setUpped) return;
        _setUpped = true;
        
        _controller = FindObjectOfType<NodeController>();
        NodeNumber = _controller.GetNextNodeNumber();
        if (_controller == null) throw new MissingComponentException("No NodeController on the scene");

        foreach (var node in connectedAtStart)
        {
            if (node == null) continue;

            ConnectToNode(node);
        }
        
        SetActive(isStartNode);
    }

    private LineRenderer CreateConnection(MapNode node)
    {
        var position = transform.position;
        var nodePosition = node.transform.position;
        var lineRenderer = Instantiate(linePrefab, transform, true).GetComponent<LineRenderer>();
        var lineDirection = position - nodePosition;
        lineDirection.Normalize();
        lineRenderer.SetPosition(0, position - lineDirection * arrowOffsetRadius);
        lineRenderer.SetPosition(1, nodePosition + lineDirection * arrowOffsetRadius);
        return lineRenderer;
    }

    private void BreakConnection(LineRenderer line)
    {
        Destroy(line);
    }

    private void UpdateMaterial()
    {
        if (_isActive)
        {
            GetComponent<Renderer>().material = enabledMaterial;
            iconBackground.color = enabledMaterial.color;
        }
        else
        {
            GetComponent<Renderer>().material = _inPath ? pathMaterial : disabledMaterial;
            iconBackground.color = _inPath ? pathMaterial.color : disabledMaterial.color;
        }
    }

    private void CheckPlayerHere()
    {
        var player = FindObjectOfType<MapPlayer>();
        var playerPos = player.transform.position;
        var nodePos = transform.position;

        var playerPos2d = new Vector2(playerPos.x, playerPos.y);
        var nodePos2d = new Vector2(nodePos.x, nodePos.y);

        var distance = (playerPos2d - nodePos2d).magnitude;

        if (distance < arrowOffsetRadius)
        {
            SwitchNodes(player);
        }
    }

    public void SetActive(bool state)
    {
        GetComponent<Collider>().enabled = state;
        _isActive = state;
        if (state)
        {
            UpdateMaterial();
            GetController().AddActiveNode(this);
            FindObjectOfType<MapPlayer>().GetComponent<DragAndDrop>().onDrop.AddListener(CheckPlayerHere);
        }
        else
        {
            UpdateMaterial();
            GetController().RemoveActiveNode(this);
            FindObjectOfType<MapPlayer>().GetComponent<DragAndDrop>().onDrop.RemoveListener(CheckPlayerHere);
        }
    }

    public void AddToPath()
    {
        _inPath = true;
        FindObjectOfType<MapPlayer>()?.AddToHistory(NodeNumber);
        UpdateMaterial();
    }

    private void ActivateNext()
    {
        foreach (var node in _connectedNodes.Keys)
        {
            if (node == null) continue;

            node.SetActive(true);
        }
    }

    public void SwitchNodesWithoutEnter(MapPlayer player)
    {
        GetController().DisableAllNodes();
        player.SetCurrentNode(NodeNumber);
        AddToPath();
        ActivateNext();
    }
    
    public void SwitchNodes(MapPlayer player)
    {
        GetController().DisableAllNodes();
        player.SetCurrentNode(NodeNumber);
        AddToPath();
        ActivateNext();
        
        // Enter the node here for now 
        // TODO: move to separate class/optimize otherwise
        EnterNode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var node in connectedAtStart)
        {
            if (node == null) continue;
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    public void SetType(ENodeType type)
    {
        _type = type;
        switch (_type)
        {
            case ENodeType.Combat:
                iconRenderer.sprite = combatIcon;
                break;
            case ENodeType.NonCombat:
                iconRenderer.sprite = chestIcon;
                break;
            case ENodeType.Win:
                iconRenderer.sprite = crownIcon;
                break;
            default:
                iconRenderer.sprite = null;
                break;
        }
    }

    public void ConnectToNode(MapNode node)
    {
        var lineRenderer = CreateConnection(node);
        _connectedNodes.Add(node, lineRenderer);
    }

    public void DisconnectNode(MapNode node)
    {
        if (!_connectedNodes.ContainsKey(node)) throw new ArgumentException("Node is not connected");
        BreakConnection(_connectedNodes[node]);
        _connectedNodes.Remove(node);
    }

    private NodeController GetController()
    {
        if (_controller != null) return _controller;
        
        
        _controller = FindObjectOfType<NodeController>();
        return _controller;
    }

    public bool IsConnectedTo(MapNode node)
    {
        return _connectedNodes.ContainsKey(node);
    }

    private void EnterNode()
    {
        switch (_type)
        {
            case ENodeType.Combat:
                SceneManager.LoadScene("GameScene");
                break;
            case ENodeType.NonCombat:
                SceneManager.LoadScene("ChestScene");
                break;
            case ENodeType.Win:
                FindObjectOfType<WinTrigger>()?.Activate();
                break;
            default:
                print("This node is not implemented yet");
                break;
        }
    }
}
