using System.Collections;
using System.Linq;
using UnityEngine;

namespace Model.GameSteps
{
    internal class ExecuteFightStep : GameStep
    {
        public ExecuteFightStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var heroParty = State.HeroParty;
            var monsterParty = State.Monsters.FirstOrDefault(party => party.CurrentNode == heroParty.CurrentNode);

            if (monsterParty != null)
            {
                State.BatteRunning = true;
                Battle.Run(heroParty, monsterParty, monsterParty.IsHidden);
                State.BatteRunning = false;
            }

            while (!Complete)
            {
                yield return new WaitForSeconds(0.1f);

                if (!State.BatteRunning)
                {
                    if (!State.HeroParty.Any(h => h.IsAlive))
                    {
                        State.RoundFinished = true;
                    }

                    Complete = true;
                }
            }
        }
    }
}