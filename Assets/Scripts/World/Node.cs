using System;

namespace World
{
    [Serializable]
    public struct Node 
    {
        public int Id;

        public Node(int id) : this()
        {
            Id = id;
        }

        public bool Equals(Node other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Node && Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Node left, Node right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !left.Equals(right);
        }
    }
}