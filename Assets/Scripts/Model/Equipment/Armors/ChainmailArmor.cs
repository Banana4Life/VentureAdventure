namespace Model.Equipment.Armors
{
    public class ChainmailArmor : ArmorBase
    {
        protected override int DamageReduction
        {
            get { return 3; }
        }

        public override int Cost
        {
            get { return 50; }
        }
    }
}