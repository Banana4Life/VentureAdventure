using System.Collections;
using System.Linq;
using Model.Util;
using Model.World;
using UnityEngine;

namespace Model.GameSteps
{
    internal class MovePlayerPartyStep : GameStep
    {
        private Node _lastNode;
        private Party HeroParty
        {
            get { return State.HeroParty; }
        }

        public MovePlayerPartyStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var node = SelectNextPlayerNode();
            _lastNode = HeroParty.CurrentNode;
            HeroParty.CurrentNode = node;

            foreach (var hero in HeroParty)
            {
                hero.KnownConnections.Add(new Connection(_lastNode, node));
            }

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
            var knownPath = HeroParty.KnownPathTo(State.SelectedTarget);
            if (knownPath != null)
            {
                return knownPath.Skip(1).First();
            }

            var neighbors = State.WorldGraph.GetNeighborsOf(HeroParty.CurrentNode);

            var nonBacktrackNodes = neighbors
                .Where(n => n != _lastNode)
                .DefaultIfEmpty(neighbors.First())
                .ToList();

            var explorationNodes = nonBacktrackNodes
                .Where(n => !HeroParty.IsKnownConnection(HeroParty.CurrentNode, n))
                .ToList();

            return explorationNodes.Any()
                ? explorationNodes.Random()
                : nonBacktrackNodes.Random();
        }
    }
}