﻿using System.Collections.Generic;
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
        
        // Use this for initialization
        void Start ()
        {
            foreach (var connection in Connections)
            {
                CreateLine(connection.Start, connection.End);
            }

            List<Unit> units = new List<Unit>();
            units.Add(new Unit {UnitClass = new FighterClass()});
            units.Add(new Unit {UnitClass = new PriestClass()});
            units.Add(new Unit {UnitClass = new RangerClass()});

            var go = GetComponent<HeroController>().SpawnHeros(units);
            var firstNeighbor = GetNeighborsOf(TavernNode).Random();
            go.AddComponent<MoveTo>().Move(firstNeighbor.transform.position, 3);
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

        public void BreakLink(NodeController left, NodeController right)
        {
        }

        public bool IsStartNode(NodeController node)
        {
            return node.Equals(TavernNode);
        }
    }
}