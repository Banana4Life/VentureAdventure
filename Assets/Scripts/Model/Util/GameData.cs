namespace Model.Util
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

        // Rewards
        public static int BaseGoldReward = 50;
        public static int GoldRewardPerDistance = 50;
        public static float GoldRewardExponent = 1.15f;
        public static int RoundCompletionExperience = 100;
        public static int BaseExperienceReward = 50;
        public static int ExperienceRewardPerDistance = 50;
        public static float ExperienceRewardExponent = 1.15f;

        // Monsters
        public static float MaxMonstersOnMap = 6;
        public static float VisibleToHiddenPartiesFactor = 3;
        public static int MaxMonstersInParty = 3;
        public static float KillExperienceFactor = 0.5f;

        // Animations
        public static float MoveAnimationTime = 1.3f;
    }
}