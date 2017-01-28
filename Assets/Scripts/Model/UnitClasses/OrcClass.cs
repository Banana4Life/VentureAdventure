namespace Model.UnitClasses
{
    public class OrcClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Orc; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Priest:
                    return Difficulty.Advantage;
                case UnitType.Fighter:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}