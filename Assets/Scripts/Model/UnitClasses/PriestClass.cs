namespace Model.UnitClasses
{
    public class PriestClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Priest; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Zombie:
                    return Difficulty.Advantage;
                case UnitType.Orc:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}