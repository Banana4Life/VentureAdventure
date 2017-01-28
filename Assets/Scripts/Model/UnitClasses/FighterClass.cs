namespace Model.UnitClasses
{
    public class FighterClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Fighter; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Orc:
                    return Difficulty.Advantage;
                case UnitType.Harpy:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}