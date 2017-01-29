using Model.Equipment;

namespace Model
{
    internal class NoArmor : ArmorBase
    {
        public override int Cost
        {
            get { return 0; }
        }

        protected override int DamageReduction
        {
            get { return 0; }
        }
    }
}