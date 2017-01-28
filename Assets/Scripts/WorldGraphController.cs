using System.Collections.Generic;
using UnityEngine;

public class WorldGraphController : MonoBehaviour
{
    public GameObject connectionPrefab;
    public GameObject startNodeObject;

    public List<NodeController> nodes = new List<NodeController>();
    private NodeController start;


    private readonly Dictionary<Pair<NodeController, NodeController>, GameObject> connections = new Dictionary<Pair<NodeController, NodeController>, GameObject>();

    // Use this for initialization
	void Start ()
	{
	    start = startNodeObject.GetComponent<NodeController>();
	    foreach (var left in nodes)
	    {
	        foreach (var right in nodes)
	        {
	            var connection = new Pair<NodeController, NodeController>(left, right);
	            var inverse = connection.flip();
	            if (left != right && !(connections.ContainsKey(connection) || connections.ContainsKey(inverse)) && isConnected(left, right))
	            {
	                var line = CreateLine(left, right);
	                connections[connection] = line;
	                connections[inverse] = line;
	            }
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public bool isConnected(NodeController left, NodeController right)
    {
        return left.neighbors.Contains(right) || right.neighbors.Contains(left);
    }

    private void InitializeNode(NodeController node)
    {
        node.node = new Node();
    }

    private void AddIfUnknown(NodeController node)
    {
        if (!nodes.Contains(node))
        {
            InitializeNode(node);
            nodes.Add(node);
        }
    }

    private GameObject CreateLine(NodeController left, NodeController right)
    {
        var connectionLine = Instantiate(connectionPrefab);
        connectionLine.transform.parent = gameObject.transform;
        var lineRenderer = connectionLine.GetComponent<LineRenderer>();
        lineRenderer.SetPositions(new []{left.gameObject.transform.position, right.gameObject.transform.position});
        return connectionLine;
    }

    public void Link(NodeController left, NodeController right)
    {
        AddIfUnknown(left);
        AddIfUnknown(right);

        left.neighbors.Add(right);
        right.neighbors.Add(left);
    }

    public void BreakLink(NodeController left, NodeController right)
    {
        left.neighbors.Remove(right);
        right.neighbors.Remove(left);
    }

    public bool IsStartNode(NodeController node)
    {
        return node.Equals(start);
    }

    public void SetStartNode(NodeController node)
    {
        start = node;
    }
}