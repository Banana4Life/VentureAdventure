namespace Model.Weapons
{
    public class StickWeapon : WeaponBase
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