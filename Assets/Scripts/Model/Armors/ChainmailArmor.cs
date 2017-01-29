namespace Model.Armors
{
    public class ChainmailArmor : Armor
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