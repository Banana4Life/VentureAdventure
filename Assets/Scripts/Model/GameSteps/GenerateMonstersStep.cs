using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Generators;
using Model.Util;
using Model.World;
using UnityEngine;

namespace Model.GameSteps
{
    public class GenerateMonstersStep : GameStep
    {
        private readonly MonsterGenerator _monsterGenerator = new MonsterGenerator();

        public GenerateMonstersStep(GameState state) : base(state)
        {
        }

        protected override IEnumerator DoLoop()
        {
            var monsterNodes = new HashSet<Node>();
            var max = Mathf.CeilToInt((Random.value + 0.3f) * GameData.MaxMonstersOnMap);
            for (var i = 0; i < max; i++)
            {
                var node = State.WorldGraph.Nodes.Random();
                if (node == State.WorldGraph.TavernNode)
                {
                    i--;
                    continue;
                }

                if (State.Objectives.Any(o => o.Node == node))
                {
                    i--;
                    continue;
                }

                monsterNodes.Add(node);
            }

            State.Monsters = monsterNodes.Select(CreateMonsterParty).ToList();
            
            Complete = true;

            yield break;
        }

        private Party CreateMonsterParty(Node node)
        {
            var party = new Party
            {
                CurrentNode = node
            };

            var monsters = _monsterGenerator.GenerateMonsters(Random.Range(1, GameData.MaxMonstersInParty + 1));
            foreach (var monster in monsters)
            {
                party.AddMember(monster);
            }

            party.IsHidden = Random.Range(0, 2) > 0;

            return party;
        }
    }
}