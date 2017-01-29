using System.Collections;
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
            if (objective != null)
            {
                objective.IsClaimed = true;
                var units = State.HeroParty.ToList();
                float reward = 0;
                foreach (var unit in units)
                {
                    reward += objective.GoldReward / (float) units.Count * (unit.Stake / 100f);
                }

                State.Money += Mathf.RoundToInt(reward);
                GameObject.Find("CoinSound").GetComponent<AudioSource>().Play();
            }

            Complete = true;

            yield break;
        }
    }
}