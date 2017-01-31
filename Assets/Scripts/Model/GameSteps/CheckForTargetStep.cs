using System.Collections;
using Model.Util;

namespace Model.GameSteps
{
    internal class CheckForTargetStep : GameStep
    {
        public CheckForTargetStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var currentNode = State.HeroParty.CurrentNode;

            if (currentNode == State.SelectedTarget)
            {
                if (currentNode == State.WorldGraph.TavernNode)
                {
                    State.RoundFinished = true;
                    State.HeroParty.AwardExperienceEach(GameData.RoundCompletionExperience);
                }
                else
                {
                    State.SelectedTarget = State.WorldGraph.TavernNode;
                }
            }

            Complete = true;

            yield break;
        }
    }
}