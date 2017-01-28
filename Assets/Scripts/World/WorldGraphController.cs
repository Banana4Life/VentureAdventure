using System.Collections.Generic;
using System.Linq;
using Model;
using Model.UnitClasses;
using UnityEngine;
using Util;

namespace World
{
    
    public class WorldGraphController : MonoBehaviour
    {
        public GameObject ConnectionPrefab;
        public NodeController TavernNode;
        public List<Connection> Connections = new List<Connection>();
        public List<NodeController> Nodes;

        // Use this for initialization
        void Start ()
        {
            foreach (var connection in Connections)
            {
                CreateLine(connection.Start, connection.End);
            }

            Nodes = Connections.SelectMany(con => new List<NodeController> {con.Start, con.End}).Distinct().ToList();

            List<Unit> units = new List<Unit>();
            units.Add(new Unit {UnitClass = new FighterClass()});
            units.Add(new Unit {UnitClass = new PriestClass()});
            units.Add(new Unit {UnitClass = new RangerClass()});

            GetComponent<HeroController>().SpawnHeros(units, this);
        }

        public bool IsConnected(NodeController first, NodeController second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            return Connections.Any(c => c.Start == lower && c.End == higher);
        }

        public List<NodeController> GetNeighborsOf(NodeController node)
        {
            return Connections
                .Where(c => c.Start == node || c.End == node)
                .Select(c => c.Start == node ? c.End : c.Start)
                .ToList();
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

        public void BreakLink(NodeController first, NodeController second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            if (IsConnected(lower, higher))
            {
                Connections.Remove(new Connection {Start= lower, End = higher});
            }
        }

        public bool IsStartNode(NodeController node)
        {
            return node.Equals(TavernNode);
        }

        public int TavernDistance(NodeController node)
        {
            throw new System.NotImplementedException();
        }
    }
}