namespace Model
{
    public class Armor
    {
        public int DamageReduction { get; set; }

        public int GetDamageReduction(int level)
        {
            return DamageReduction;
        }
    }
}