using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Util;
using Model.World;
using UnityEngine;

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

        public bool IsAlive
        {
            get { return this.Any(member => member.IsAlive); }
        }

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

        public bool IsKnownConnection(Node first, Node second)
        {
            return this.Any(member => member.KnownConnections.Contains(new Connection(first, second)));
        }

        public void AwardKillExperience(Unit unit)
        {
            var killExp = unit.Experience*GameData.KillExperienceFactor;

            this.AwardExperienceShared(Mathf.CeilToInt(killExp));
        }

        public void AwardExperienceShared(int experience)
        {

            var perMemberExp = experience / this.Count(m => m.IsAlive);

            foreach (var member in this.Where(m => m.IsAlive))
            {
                member.GrantExperience(Mathf.CeilToInt(perMemberExp));
            }
        }

        public void AwardExperienceEach(int experience)
        {
            foreach (var member in this.Where(m => m.IsAlive))
            {
                member.GrantExperience(experience);
            }
        }
    }
}