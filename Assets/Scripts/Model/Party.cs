using System.Collections;
using System.Collections.Generic;
using Model.World;

namespace Model
{
    public class Party : IEnumerable<Unit>
    {
        public HashSet<int> MapKnowledge { get; private set; }
        private readonly List<Unit> _units = new List<Unit>();

        public bool KnowsPathToTarget { get; set; }
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