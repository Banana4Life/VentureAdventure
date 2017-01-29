namespace Model.Weapons
{
    public class Sword : Weapon
    {
        protected override int Damage
        {
            get { return 3; }
        }

        public override int Cost
        {
            get { return 35; }
        }
    }
}