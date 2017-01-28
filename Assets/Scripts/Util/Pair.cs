using System.Collections.Generic;

namespace Util
{
    public class Pair<L, R>
    {
        public readonly L left;
        public readonly R right;

        public Pair(L left, R right)
        {
            this.left = left;
            this.right = right;
        }

        public Pair<R, L> flip()
        {
            return new Pair<R, L>(right, left);
        }

        protected bool Equals(Pair<L, R> other)
        {
            return EqualityComparer<L>.Default.Equals(left, other.left) && EqualityComparer<R>.Default.Equals(right, other.right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pair<L, R>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<L>.Default.GetHashCode(left) * 397) ^ EqualityComparer<R>.Default.GetHashCode(right);
            }
        }
    }
}
