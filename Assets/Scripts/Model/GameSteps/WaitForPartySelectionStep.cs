using System.Collections;
using UnityEngine;

namespace Model.GameSteps
{
    public class WaitForPartySelectionStep : GameStep
    {
        public WaitForPartySelectionStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            while (!Complete)
            {
                if (State.HeroParty != null)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}