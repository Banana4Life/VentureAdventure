namespace Model.UnitClasses
{
    public class HarpyClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Harpy; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Fighter:
                    return Difficulty.Advantage;
                case UnitType.Ranger:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}