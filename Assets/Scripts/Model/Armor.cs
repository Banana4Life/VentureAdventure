namespace Model
{
    public abstract class Armor : Equipment
    {
        protected abstract int DamageReduction { get; }

        public virtual int GetDamageReduction(int level)
        {
            return DamageReduction;
        }
    }
}