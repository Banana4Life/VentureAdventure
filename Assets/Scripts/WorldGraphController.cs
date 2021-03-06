﻿using System.Collections.Generic;
using System.Linq;
using Model;
using Model.UnitClasses;
using Model.World;
using UnityEngine;

[ExecuteInEditMode]
public class WorldGraphController : MonoBehaviour
{
    public GameObject ConnectionPrefab;
    public NodeController TavernNodeController;

    [SerializeField]
    public WorldGraph WorldGraph = new WorldGraph();

    private Dictionary<Node, NodeController> _nodeControllers; 

    // Use this for initialization
    void Start ()
    {
        if (!Application.isPlaying) return;

        _nodeControllers = GetComponentsInChildren<NodeController>().ToDictionary(nc => nc.Node);
        
        foreach (var connection in WorldGraph.Connections)
        {
            var startController = _nodeControllers[connection.Start];
            var endController = _nodeControllers[connection.End];


            CreateLine(startController, endController);
        }

        WorldGraph.TavernNode = TavernNodeController.Node;
    }

    public void Update()
    {
        if (Application.isPlaying) return;

        _nodeControllers = GetComponentsInChildren<NodeController>().ToDictionary(nc => nc.Node);

        foreach (var connection in WorldGraph.Connections)
        {
            var startController = _nodeControllers[connection.Start];
            var endController = _nodeControllers[connection.End];
            Debug.DrawLine(startController.transform.position, endController.transform.position);
        }
    }

    private GameObject CreateLine(NodeController left, NodeController right)
    {
        var connectionLine = Instantiate(ConnectionPrefab);
        connectionLine.transform.SetParent(gameObject.transform);
        var lineRenderer = connectionLine.GetComponent<LineRenderer>();
        lineRenderer.SetPositions(new []{left.gameObject.transform.position, right.gameObject.transform.position});
        return connectionLine;
    }

    public Vector3 GetNodePositionOnMap(Node node)
    {
        return _nodeControllers[node].transform.position;
    }
}