namespace Model
{
    public abstract class Armor
    {
        protected abstract int DamageReduction { get; }

        public virtual int GetDamageReduction(int level)
        {
            return DamageReduction;
        }
    }
}