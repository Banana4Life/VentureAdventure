using System;
using System.Collections.Generic;
using Model;
using Model.UnitClasses;
using UnityEngine;
using Util;

namespace World
{
    public class WorldGraphController : MonoBehaviour
    {
        public GameObject connectionPrefab;
        public GameObject startNodeObject;

        public GameObject fighterPrefab;
        public GameObject priestPrefab;
        public GameObject rangerPrefab;

        public List<NodeController> nodes = new List<NodeController>();
        private NodeController start;


        private readonly Dictionary<Pair<NodeController, NodeController>, GameObject> connections = new Dictionary<Pair<NodeController, NodeController>, GameObject>();

        // Use this for initialization
        void Start ()
        {
            start = startNodeObject.GetComponent<NodeController>();
            Debug.LogWarning("node count: " + nodes.Count);
            foreach (var left in nodes)
            {
                foreach (var right in nodes)
                {
                    var connection = new Pair<NodeController, NodeController>(left, right);
                    var inverse = connection.flip();
                    if (left != right && !(connections.ContainsKey(connection) || connections.ContainsKey(inverse)) && IsConnected(left, right))
                    {
                        Debug.LogWarning("Connect " + left.gameObject + " and " + right.gameObject);
                        var line = CreateLine(left, right);
                        connections[connection] = line;
                        connections[inverse] = line;
                    }
                }
            }

            var units = new List<Unit>();
            var unit = new Unit {UnitClass = new FighterClass()};
            units.Add(unit);
            SpawnHeros(units);
        }

        // Update is called once per frame
        void Update () {

        }

        public bool IsConnected(NodeController left, NodeController right)
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

        public void SpawnHeros(List<Unit> fighters)
        {
            foreach (var fighter in fighters)
            {
                var unitType = fighter.UnitClass.UnitType;
                GameObject unitPrefab;
                switch (unitType)
                {
                    case UnitType.Fighter:
                        unitPrefab = fighterPrefab;
                        break;
                    case UnitType.Priest:
                        unitPrefab = fighterPrefab;
                        break;
                    case UnitType.Ranger:
                        unitPrefab = fighterPrefab;
                        break;
                    default:
                        Debug.LogError("unknown unit type " + unitType);
                        throw new Exception("unknown unit type");
                }

                var unitObject = Instantiate(unitPrefab);
                unitObject.transform.parent = transform;
                unitObject.transform.localPosition = start.gameObject.transform.position;

            }
        }
    }
}