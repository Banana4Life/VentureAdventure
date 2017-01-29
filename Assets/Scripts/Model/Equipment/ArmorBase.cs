namespace Model
{
    public abstract class ArmorBase : EquipmentBase
    {
        protected abstract int DamageReduction { get; }

        public virtual int GetDamageReduction(int level)
        {
            return DamageReduction;
        }
    }
}