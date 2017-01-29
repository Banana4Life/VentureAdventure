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

        public override IEnumerator DoLoop()
        {
            var heroParty = State.HeroParty;
            var monsterParty = State.Monsters.FirstOrDefault(party => party.CurrentNode == heroParty.CurrentNode);

            if (monsterParty != null)
            {
                Battle.Run(heroParty, monsterParty, monsterParty.IsHidden);
                State.BatteRunning = true;
            }

            while (!Complete)
            {
                if (!State.BatteRunning)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}