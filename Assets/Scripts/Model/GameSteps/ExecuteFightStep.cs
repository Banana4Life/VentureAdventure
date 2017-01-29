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
            }

            
            yield return new WaitForSeconds(1f);

            State.BatteRunning = false;
            if (!State.HeroParty.Any(h => h.IsAlive))
            {
                State.RoundFinished = true;
            }

            Complete = true;
        }
    }
}