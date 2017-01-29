namespace Model
{
    public abstract class Weapon : Equipment
    {
        protected abstract int Damage { get; }

        public virtual int GetDamage(int characterLevel)
        {
            return Damage;
        }
    }
}