using System.Collections;
using UnityEngine;
using World;

namespace Model
{
    internal class MovePlayerPartyStep : GameStep
    {
        public MovePlayerPartyStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;

            var node = SelectNextPlayerNode();
            State.HeroParty.CurrentNode = node;
            State.HeroPartyMoving = true;

            while (!Complete)
            {
                if (!State.HeroPartyMoving)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }

        }

        private Node SelectNextPlayerNode()
        {
            return default(Node);
        }
    }
}