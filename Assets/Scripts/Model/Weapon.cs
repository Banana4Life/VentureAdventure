namespace Model
{
    public class Weapon
    {
        public int Damage { get; set; }

        public int GetDamage(int characterLevel)
        {
            return Damage;
        }
    }
}