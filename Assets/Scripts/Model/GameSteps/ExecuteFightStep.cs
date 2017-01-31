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

            if (monsterParty != null && monsterParty.IsAlive)
            {
                var isSurpriseBattle = monsterParty.IsHidden;
                monsterParty.IsHidden = false;

                yield return RunBattle(heroParty, monsterParty, isSurpriseBattle);

                DealWithAftermath(monsterParty, heroParty);
            }

            Complete = true;
        }

        private void DealWithAftermath(Party monsterParty, Party heroParty)
        {
            if (!State.HeroParty.IsAlive)
            {
                State.RoundFinished = true;
            }
            else
            {
                State.Monsters.Remove(monsterParty);
                foreach (var monster in monsterParty)
                {
                    heroParty.AwardKillExperience(monster);
                }
            }
        }

        private IEnumerator RunBattle(Party heroParty, Party monsterParty, bool isSurpriseBattle)
        {
            State.BatteRunning = true;
            Battle.Run(heroParty, monsterParty, isSurpriseBattle);

            yield return new WaitForSeconds(1f);

            State.BatteRunning = false;
        }
    }
}