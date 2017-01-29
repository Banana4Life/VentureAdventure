using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using Util;

namespace World
{
    [Serializable]
    public class WorldGraph
    {
        [SerializeField]
        public List<Connection> Connections = new List<Connection>();

        [SerializeField]
        public List<Node> Nodes = new List<Node>();

        [SerializeField]
        public Node TavernNode { get; set; }

        public bool IsConnected(Node first, Node second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            return Connections.Any(c => c.Start == lower && c.End == higher);
        }

        public List<Node> GetNeighborsOf(Node node)
        {
            return Connections
                .Where(c => c.Start == node || c.End == node)
                .Select(c => c.Start == node ? c.End : c.Start)
                .ToList();
        }

        public void CreateConnection(Node first, Node second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            if (!IsConnected(lower, higher))
            {
                Connections.Add(new Connection { Start = lower, End = higher });
            }

            AddNode(lower);
            AddNode(higher);
        }

        private void AddNode(Node value)
        {
            if (!Nodes.Contains(value))
            {
                Nodes.Add(value);
            }
        }

        public void RemoveConnection(Node first, Node second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            if (IsConnected(lower, higher))
            {
                Connections.Remove(new Connection { Start = lower, End = higher });
            }
        }

        public bool IsStartNode(Node node)
        {
            return node == TavernNode;
        }

        public int TavernDistance(Node node)
        {
            return DistanceBetween(TavernNode, node);
        }

        private int DistanceBetween(Node first, Node second)
        {
            return PathFinder.FindDistance(this, first, second);
        }
    }
}