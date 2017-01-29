using System.Collections;

namespace Model.GameSteps
{
    internal class PickupLootStep : GameStep
    {
        public PickupLootStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = true;

            yield break;
        }
    }
}