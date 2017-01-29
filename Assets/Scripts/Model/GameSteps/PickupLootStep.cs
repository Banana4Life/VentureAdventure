using System.Collections;

namespace Model
{
    internal class PickupLootStep : GameStep
    {
        public PickupLootStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            yield break;
        }
    }
}