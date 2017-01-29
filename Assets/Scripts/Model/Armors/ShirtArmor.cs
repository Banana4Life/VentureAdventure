namespace Model.Armors
{
    public class ShirtArmor : Armor
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