using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace World
{
    public class HeroController : MonoBehaviour
    {
        private WorldGraphController _worldGraphController;
        public GameObject fighterPrefab;
        public GameObject priestPrefab;
        public GameObject rangerPrefab;

        private void Start()
        {
            _worldGraphController = GetComponent<WorldGraphController>();
        }

        public GameObject SpawnHeros(List<Unit> fighters, WorldGraphController graph)
        {
            var heroContainer = new GameObject("Hero Container");
            heroContainer.transform.parent = transform;
            heroContainer.transform.position = graph.TavernNode.gameObject.transform.position;


            foreach (var fighter in fighters)
            {
                var unitType = fighter.UnitClass.UnitType;
                GameObject unitPrefab;
                switch (unitType)
                {
                    case UnitType.Fighter:
                        unitPrefab = fighterPrefab;
                        break;
                    case UnitType.Priest:
                        unitPrefab = priestPrefab;
                        break;
                    case UnitType.Ranger:
                        unitPrefab = rangerPrefab;
                        break;
                    default:
                        Debug.LogError("unknown unit type " + unitType);
                        throw new Exception("unknown unit type");
                }

                var unitObject = Instantiate(unitPrefab);
                unitObject.transform.parent = heroContainer.transform;
                unitObject.transform.localPosition = Vector3.zero;
            }

            var mover = heroContainer.AddComponent<TestNodeMover>();
            mover.graph = _worldGraphController;
            mover.currentNode = graph.TavernNode;

            return heroContainer;
        }
    }
}