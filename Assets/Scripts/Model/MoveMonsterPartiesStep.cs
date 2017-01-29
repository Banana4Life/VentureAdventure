using System.Collections;
using UnityEngine;
using World;

namespace Model
{
    internal class MoveMonsterPartiesStep : GameStep
    {
        public MoveMonsterPartiesStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;

            //TODO moveMonsters
            
            while (!Complete)
            {
                if (!State.MonsterPartiesMoving)
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