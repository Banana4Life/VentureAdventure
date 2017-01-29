using System.Collections;
using System.Collections.Generic;
using World;

namespace Model
{
    public class Party : IEnumerable<Unit>
    {
        public HashSet<int> MapKnowledge { get; private set; }
        private List<Unit> _units;

        public List<Unit> Units
        {
            get { return _units; }
            set
            {
                _units = value;

                var knowledge = new HashSet<int>();
                foreach (var unit in value)
                {
                    knowledge.UnionWith(unit.MapKnowledge);
                }

                MapKnowledge = knowledge;
            }
        }

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