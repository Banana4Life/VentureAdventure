namespace Model.Equipment.Armors
{
    public class ShirtArmor : ArmorBase
    {
        protected override int DamageReduction
        {
            get { return 0; }
        }

        public override int Cost
        {
            get { return 5; }
        }
    }
}