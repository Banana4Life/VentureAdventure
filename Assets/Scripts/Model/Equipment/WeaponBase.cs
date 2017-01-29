namespace Model
{
    public abstract class WeaponBase : EquipmentBase
    {
        protected abstract int Damage { get; }

        public virtual int GetDamage(int characterLevel)
        {
            return Damage;
        }
    }
}