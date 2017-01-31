using Model.Util;
using Model.World;
using UnityEngine;

namespace Model.Generators
{
    internal class ObjectiveGenerator
    {
        public Objective GenerateObjective(int tavernDistance, Node node)
        {
            return new Objective
            {
                ObjectiveType = ObjectiveType.Treasure,
                GoldReward = GameData.BaseGoldReward + Mathf.CeilToInt(Mathf.Pow(GameData.GoldRewardPerDistance, GameData.GoldRewardExponent)*tavernDistance),
                Node = node,
                Experience = GameData.BaseExperienceReward + Mathf.CeilToInt(Mathf.Pow(GameData.ExperienceRewardPerDistance, GameData.ExperienceRewardExponent)*tavernDistance)
            };
        }
    }
}