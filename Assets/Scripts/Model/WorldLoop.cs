using System.Collections.Generic;
using UnityEngine;
using World;

namespace Model
{
    [RequireComponent(typeof(WorldGraphController))]
    public class WorldLoop : MonoBehaviour
    {
        private WorldGraphController _graphController;
        public GameObject ObjectiveControllerPrefab;
        public GameObject PartyContainerPrefab;
        public GameObject UnitVisualizerPrefab;
        private ObjectiveGenerator _objectiveGenerator = new ObjectiveGenerator();
        private MonsterGenerator _monsterGenerator = new MonsterGenerator();

        void Start()
        {
            _graphController = GetComponent<WorldGraphController>();
        }

        void Update()
        {
            GenerateObjectives();
            GenerateMonsters();
            //WaitForObjectiveSelection();
            //WaitForPartySelection();

            //var party = SpawnParty();

            //while (party.IsAlive)
            //{
            //    MoveParty(party);
            //    MoveMonsters();

            //    TriggerEvents();

            //    if (party.ReachedTarget)
            //    {
            //        party.SetTarget(TavernNode);
            //    }
            //}
        }

        private void GenerateMonsters()
        {
            var monsterNodes = new HashSet<NodeController>();
            for (int i = 0; i < Mathf.CeilToInt((Random.value + 0.3f) * GameData.MaxMonstersOnMap); i++)
            {
                var node = _graphController.Nodes.Random();
                monsterNodes.Add(node);
            }

            foreach (var node in monsterNodes)
            {
                CreateMonsterParty(node);
                
            }
        }

        private void CreateMonsterParty(NodeController node)
        {
            var partyGameObject = Instantiate(PartyContainerPrefab);
            var partyContainer = partyGameObject.GetComponent<PartyContainer>();

            var monsters = _monsterGenerator.GenerateMonsters(Random.Range(1, GameData.MaxMonstersInParty + 1));
            foreach (var monster in monsters)
            {
                var unitObject = Instantiate(UnitVisualizerPrefab);
                var visualizer = unitObject.GetComponent<UnitVisualizer>();
                visualizer.Unit = monster;
                partyContainer.Members.Add(visualizer);
            }

            partyContainer.NodeController = node;
            partyContainer.IsHiddenParty = Random.Range(0, 2) > 0;
        }

        private void GenerateObjectives()
        {
            var objectiveNodes = new HashSet<NodeController>();
            for (int i = 0; i < Mathf.CeilToInt(Random.value*GameData.MaxTreasures); i++)
            {
                var node = _graphController.Nodes.Random();
                objectiveNodes.Add(node);
            }

            foreach (var node in objectiveNodes)
            {
                var gameObject = Instantiate(ObjectiveControllerPrefab);
                var objectiveController = gameObject.GetComponent<ObjectiveController>();
                objectiveController.NodeController = node;
                objectiveController.Objective = _objectiveGenerator.GenerateObjective(_graphController.TavernDistance(node));
            }


        }

        private static void MoveParty(object party)
        {
            //if (party.KnowsPathToTarget)
            //{
            //    party.MoveTowardsTarget();
            //}
            //else
            //{
            //    party.MoveRandom();
            //}
        }
    }
}