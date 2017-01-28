namespace Model
{
    public static class GameData
    {
        // HP 
        public static int BaseHitPoints = 5;
        public static int HitPointsPerLevel = 8;
        public static float HitPointsExponent = 1.15f;

        // Damage
        public static int BaseDamage = 2;
        public static int DamagePerLevel = 3;

        // Treasures
        public static int MaxTreasures = 4;

        // Gold Reward
        public static int BaseGoldReward = 50;
        public static int GoldRewardPerDistance = 50;
        public static float GoldRewardExponent = 1.15f;

        // Monsters
        public static float MaxMonstersOnMap = 6;
        public static int MaxMonstersInParty = 3;
    }
}