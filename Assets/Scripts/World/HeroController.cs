using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace World
{
    public class HeroController : MonoBehaviour
    {
        private WorldGraphController _worldGraphController;
        public GameObject unitVisualizer;
      
        private void Start()
        {
            _worldGraphController = GetComponent<WorldGraphController>();
        }

        public GameObject SpawnHeros(List<Unit> heroes, WorldGraphController graph)
        {
            var heroContainer = new GameObject("Hero Container");
            heroContainer.transform.parent = transform;
            heroContainer.transform.position = graph.TavernNode.gameObject.transform.position;


            foreach (var hero in heroes)
            {
                var unitObject = Instantiate(unitVisualizer);
                unitObject.transform.parent = heroContainer.transform;
                unitObject.transform.localPosition = Vector3.zero;
                unitObject.GetComponent<UnitVisualizer>().Unit = hero;
            }

            var mover = heroContainer.AddComponent<TestNodeMover>();
            mover.graph = _worldGraphController;
            mover.currentNode = graph.TavernNode;

            return heroContainer;
        }
    }
}