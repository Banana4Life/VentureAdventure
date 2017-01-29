using System.Collections;

namespace Model.GameSteps
{
    internal class CheckForTargetStep : GameStep
    {
        public CheckForTargetStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;

            var currentNode = State.HeroParty.CurrentNode;

            if (currentNode == State.SelectedTarget)
            {
                if (currentNode == State.WorldGraph.TavernNode)
                {
                    State.RoundFinished = true;
                    State.PlayedRounds++;
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