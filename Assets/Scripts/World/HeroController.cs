using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace World
{
    public class HeroController : MonoBehaviour
    {        
        public GameObject UnitVisualizer;
        public GameObject HeroPartyPrefab;
      
        private void Start()
        {            
        }

        public GameObject SpawnHeros(List<Unit> heroes, WorldGraphController graph)
        {
            var heroContainer = Instantiate(HeroPartyPrefab);
            heroContainer.transform.parent = transform;

            heroContainer.transform.position = graph.TavernNodeController.gameObject.transform.position;
            var partyContainer = heroContainer.GetComponent<PartyContainer>();
            var members =  partyContainer.Members;
            partyContainer.NodeController = graph.TavernNodeController;

            foreach (var hero in heroes)
            {
                var unitObject = Instantiate(UnitVisualizer);
                unitObject.transform.parent = heroContainer.transform;
                unitObject.transform.localPosition = Vector3.zero;
                var unitVisualizer = unitObject.GetComponent<UnitVisualizer>();
                unitVisualizer.Unit = hero;
                members.Add(unitVisualizer);
            }

            var mover = heroContainer.AddComponent<TestNodeMover>();
            mover.graph = graph;
            mover.currentNode = graph.TavernNodeController;

            return heroContainer;
        }
    }
}