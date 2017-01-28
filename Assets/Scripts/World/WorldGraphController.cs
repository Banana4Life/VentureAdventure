using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace World
{
    
    public class WorldGraphController : MonoBehaviour
    {
        public GameObject ConnectionPrefab;
        public NodeController TavernNode;
        public List<Connection> Connections = new List<Connection>(); 
        
        // Use this for initialization
        void Start ()
        {
            foreach (var connection in Connections)
            {
                CreateLine(connection.Start, connection.End);
            }
        }

        public bool IsConnected(NodeController first, NodeController second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            return Connections.Any(c => c.Start == lower && c.End == higher);
        }
       

        private GameObject CreateLine(NodeController left, NodeController right)
        {
            var connectionLine = Instantiate(ConnectionPrefab);
            connectionLine.transform.parent = gameObject.transform;
            var lineRenderer = connectionLine.GetComponent<LineRenderer>();
            lineRenderer.SetPositions(new []{left.gameObject.transform.position, right.gameObject.transform.position});
            return connectionLine;
        }

        public void Link(NodeController first, NodeController second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            if (!IsConnected(lower, higher))
            {
                Connections.Add(new Connection {Start= lower, End = higher});
            }
        }

        public void BreakLink(NodeController left, NodeController right)
        {
        }

        public bool IsStartNode(NodeController node)
        {
            return node.Equals(TavernNode);
        }
    }
}