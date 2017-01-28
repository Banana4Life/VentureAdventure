namespace Model
{
    public abstract class UnitClass
    {
        public abstract UnitType UnitType { get; }
        public abstract Difficulty GetDifficulty(UnitClass unitClass);
    }
}