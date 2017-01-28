namespace Model.UnitClasses
{
    public class ZombieClass : UnitClass
    {
        public override UnitType UnitType
        {
            get { return UnitType.Zombie; }
        }

        public override Difficulty GetDifficulty(UnitClass unitClass)
        {
            switch (unitClass.UnitType)
            {
                case UnitType.Ranger:
                    return Difficulty.Advantage;
                case UnitType.Priest:
                    return Difficulty.Disadvantage;
                default:
                    return Difficulty.Equal;
            }
        }
    }
}