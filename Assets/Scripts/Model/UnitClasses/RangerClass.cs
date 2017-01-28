namespace Model.UnitClasses
{
    public class RangerClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Ranger; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Harpy:
                    return Difficulty.Advantage;
                case UnitType.Zombie:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}