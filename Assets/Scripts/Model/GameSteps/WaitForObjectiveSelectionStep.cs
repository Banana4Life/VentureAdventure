using System.Collections;
using UnityEngine;

namespace Model.GameSteps
{
    public class WaitForObjectiveSelectionStep : GameStep
    {
        public WaitForObjectiveSelectionStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;

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