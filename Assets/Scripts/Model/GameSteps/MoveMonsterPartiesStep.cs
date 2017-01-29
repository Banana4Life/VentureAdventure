using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    internal class MoveMonsterPartiesStep : GameStep
    {
        private List<Party> Parties
        {
            get { return State.Monsters; }
        } 

        public MoveMonsterPartiesStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;
            State.MovingMonsterParty = null;

            if (Random.Range(0, 2) > 0)
            {
                yield return new WaitForSeconds(GameData.MoveAnimationTime);
                Complete = true;
                yield break;
            }

            var moveParty = Parties.Where(party => 
                    State.WorldGraph.GetNeighborsOf(party.CurrentNode)
                    .Any(n => Parties.All(p => p.CurrentNode != n)))
                .DefaultIfEmpty(null)
                .ToList()
                .Random();

            if (moveParty != null)
            {
                var moveNode = State.WorldGraph.GetNeighborsOf(moveParty.CurrentNode)
                    .Where(n => Parties.All(p => p.CurrentNode != n))
                    .ToList()
                    .Random();

                moveParty.CurrentNode = moveNode;
                State.MovingMonsterParty = moveParty;
            }

            while (!Complete)
            {
                if (!State.MonsterPartyMoving)
                {
                    Complete = true;
                }

                yield return new WaitForSeconds(0.1f);
            }

        }
        
    }
}