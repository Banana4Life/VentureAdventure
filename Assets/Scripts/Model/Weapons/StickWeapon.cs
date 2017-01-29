namespace Model.Weapons
{
    public class StickWeapon : Weapon
    {
        protected override int Damage
        {
            get { return 1; }
        }

        public override int Cost
        {
            get { return 5; }
        }
    }
}