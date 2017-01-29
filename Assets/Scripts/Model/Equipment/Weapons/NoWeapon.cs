using Model.Equipment;

namespace Model
{
    internal class NoWeapon : WeaponBase
    {
        public override int Cost
        {
            get { return 0; }
        }

        protected override int Damage
        {
            get { return 0; }
        }
    }
}