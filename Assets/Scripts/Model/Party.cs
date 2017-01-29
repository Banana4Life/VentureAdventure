using System.Collections;
using System.Collections.Generic;
using Model.Util;
using Model.World;

namespace Model
{
    public class Party : IEnumerable<Unit>
    {
        private readonly List<Unit> _units = new List<Unit>();

        public List<Node> KnownPathTo(Node target)
        {
            var knownConnections = new HashSet<Connection>();
            foreach (var unit in this)
            {
                knownConnections.UnionWith(unit.KnownConnections);
            }

            var combinedGraph = new WorldGraph();
            foreach (var connection in knownConnections)
            {
                combinedGraph.CreateConnection(connection.Start, connection.End);
            }

            return PathFinder.FindPath(combinedGraph, CurrentNode, target);
        }

        public Node CurrentNode { get; set; }
        public bool IsHidden { get; set; }

        public void AddMember(Unit unit)
        {
            _units.Add(unit);

           
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}