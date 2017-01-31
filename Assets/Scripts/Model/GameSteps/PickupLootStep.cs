using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.GameSteps
{
    internal class PickupLootStep : GameStep
    {
        public PickupLootStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var objective = State.Objectives.FirstOrDefault(o => o.Node == State.HeroParty.CurrentNode);
            if (objective != null && !objective.IsClaimed)
            {
                objective.IsClaimed = true;

                var units = State.HeroParty.Where(h => h.IsAlive).ToList();
                var goldReward = units.Sum(unit => objective.GoldReward/(float) units.Count*(unit.Stake/100f));
                State.Money += Mathf.RoundToInt(goldReward);

                State.HeroParty.AwardExperienceShared(objective.Experience);
                
                GameObject.Find("CoinSound").GetComponent<AudioSource>().Play();
            }

            Complete = true;

            yield break;
        }
    }
}