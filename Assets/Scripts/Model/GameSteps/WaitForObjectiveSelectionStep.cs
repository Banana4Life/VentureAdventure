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
            while (!Complete)
            {
                if (State.SelectedTarget != null)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}