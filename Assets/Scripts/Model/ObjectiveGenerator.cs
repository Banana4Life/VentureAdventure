using UnityEngine;

namespace Model
{
    internal class ObjectiveGenerator
    {
        public Objective GenerateObjective(int tavernDistance)
        {
            return new Objective
            {
                ObjectiveType = ObjectiveType.Treasure,
                GoldReward = GameData.BaseGoldReward + Mathf.CeilToInt(Mathf.Pow(GameData.GoldRewardPerDistance, GameData.GoldRewardExponent)*tavernDistance)
            };
        }
    }
}