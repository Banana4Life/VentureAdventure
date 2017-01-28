using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace World
{
    public class HeroController
    {
        private WorldGraphController _worldGraphController;
        public GameObject fighterPrefab;
        public GameObject priestPrefab;
        public GameObject rangerPrefab;

        public HeroController(WorldGraphController worldGraphController)
        {
            _worldGraphController = worldGraphController;
        }

        public void SpawnHeros(List<Unit> fighters)
        {
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
                        unitPrefab = fighterPrefab;
                        break;
                    case UnitType.Ranger:
                        unitPrefab = fighterPrefab;
                        break;
                    default:
                        Debug.LogError("unknown unit type " + unitType);
                        throw new Exception("unknown unit type");
                }
                

            }
        }
    }
}