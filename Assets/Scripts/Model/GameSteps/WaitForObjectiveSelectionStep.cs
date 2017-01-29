using System.Collections;
using UnityEngine;

namespace Model.GameSteps
{
    public class WaitForObjectiveSelectionStep : GameStep
    {
        public WaitForObjectiveSelectionStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            while (!Complete)
            {
                if (State.TargetSelected)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}