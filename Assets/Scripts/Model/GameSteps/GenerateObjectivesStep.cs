using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Generators;
using Model.Util;
using Model.World;
using UnityEngine;

namespace Model.GameSteps
{
    public class GenerateObjectivesStep : GameStep
    {
        private readonly ObjectiveGenerator _objectiveGenerator = new ObjectiveGenerator();
        
        public GenerateObjectivesStep(GameState state) : base(state)
        {
        }

        public override IEnumerator DoLoop()
        {
            Complete = false;
            
            var objectiveNodes = new HashSet<Node>();
            for (int i = 0; i < Mathf.CeilToInt(Random.value * GameData.MaxTreasures); i++)
            {
                var node = State.WorldGraph.Nodes.Random();
                objectiveNodes.Add(node);
            }

            var objectives = objectiveNodes
                .Select(node => _objectiveGenerator.GenerateObjective(State.WorldGraph.TavernDistance(node)))
                .ToList();

            State.Objectives = objectives;

            Complete = true;

            yield break;
        }
    }
}