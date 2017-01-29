using System.Collections;
using System.Linq;

namespace Model.GameSteps
{
    internal class PickupLootStep : GameStep
    {
        public PickupLootStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var objective = State.Objectives.FirstOrDefault(o => o.Node == State.HeroParty.CurrentNode);
            if (objective != null)
            {
                objective.IsClaimed = true; 
                State.Money += objective.GoldReward;
            }

            Complete = true;

            yield break;
        }
    }
}