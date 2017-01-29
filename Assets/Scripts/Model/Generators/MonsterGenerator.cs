using System;
using System.Collections.Generic;
using Model.UnitClasses;
using Model.Util;

namespace Model.Generators
{
    class MonsterGenerator
    {
        private UnitType[] unitTypes = new[] {UnitType.Orc, UnitType.Harpy, UnitType.Zombie};

        public List<Unit> GenerateMonsters(int partyStrength)
        {
            var units = new List<Unit>();

            for (var i = 0; i < partyStrength; ++i)
            {
                var unitType = unitTypes.Random();
                var unit = new Unit();
                switch (unitType)
                {
                    case UnitType.Orc:
                        unit.UnitClass = new OrcClass();
                        unit.Name = "Orc " + i;
                        break;
                    case UnitType.Zombie:
                        unit.UnitClass = new ZombieClass();
                        unit.Name = "Zombie " + i;
                        break;
                    case UnitType.Harpy:
                        unit.UnitClass = new HarpyClass();
                        unit.Name = "Harpy " + i;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                units.Add(unit);
            }


            return units;
        }
    }
}
