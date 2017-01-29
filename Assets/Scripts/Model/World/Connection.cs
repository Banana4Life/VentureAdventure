using System;

namespace Model.World
{
    [Serializable]
    public class Connection
    {
        public Node Start;
        public Node End;

        public Connection()
        {
        }

        public Connection(Node first, Node second)
        {
            var lower = first.Id < second.Id ? first : second;
            var higher = first.Id > second.Id ? first : second;

            this.Start = lower;
            this.End = higher;
        }

        protected bool Equals(Connection other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Connection) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return string.Format("Start: {0}, End: {1}", Start.Id, End.Id);
        }
    }
}