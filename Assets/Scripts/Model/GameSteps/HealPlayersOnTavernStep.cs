using System.Collections;
using System.Linq;
using UnityEngine;

namespace Model.GameSteps
{
    internal class HealPlayersOnTavernStep : GameStep
    {
        public HealPlayersOnTavernStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            if (State.HeroParty.CurrentNode == State.WorldGraph.TavernNode)
            {
                GameObject.Find("ClickSound3").GetComponent<AudioSource>().Play();
                foreach (var unit in State.HeroParty.Where(u => u.IsAlive))
                {
                    unit.RegenerateHitPoints();
                }
            }

            Complete = true;

            yield break;
        }
    }
}