namespace Model.Equipment.Weapons
{
    public class Sword : WeaponBase
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