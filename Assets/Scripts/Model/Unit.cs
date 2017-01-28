using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Unit
    {
        public Unit()
        {
            this.CurrentHitPoints = MaxHitPoints;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1} Lvl.{4} - {2}/{3}]", Name, UnitClass.UnitType, CurrentHitPoints, MaxHitPoints, Level);
        }

        public string Name { get; set; }

        public int Level
        {
            get
            {
                if (Experience < 100) return 1;
                if (Experience < 250) return 2;
                if (Experience < 500) return 3;
                if (Experience < 850) return 4;
                if (Experience < 1300) return 5;
                if (Experience < 1800) return 6;
                if (Experience < 2500) return 7;
                if (Experience < 3500) return 8;
                return Experience < 5000 ? 9 : 10;
            }
        }

        public int Experience { get; private set; }

        public UnitClass UnitClass { get; set; }
        public int CurrentHitPoints { get; private set; }
        public bool IsAlive { get { return CurrentHitPoints > 0; } }

        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }
        
        public int MaxHitPoints
        {
            get { return GameData.BaseHitPoints + Level * GameData.HitPointsPerLevel; }
        }

        public int Damage
        {
            get { return GameData.BaseDamage + Level * GameData.DamagePerLevel + Weapon.GetDamage(Level); }
        }

        public int DamageReduction
        {
            get { return Armor.GetDamageReduction(Level); }
        }

        public void GrantExperience(int experience)
        {
            var levelBefore = this.Level;
            this.Experience += experience;
            if (levelBefore != this.Level)
            {
                this.CurrentHitPoints = this.MaxHitPoints;
            }
        }

        public void Attack(IList<Unit> units)
        {
            var unitToAttack = ChooseUnit(units);
            var difficulty = this.UnitClass.GetDifficulty(unitToAttack.UnitClass);

            switch (difficulty)
            {
                case Difficulty.Advantage:
                        //unitToAttack.ReceiveDamage();
                    break;
                case Difficulty.Disadvantage:
                    break;
                case Difficulty.Equal:
                default:
                    break;
            }


            

            Debug.Log(string.Format("{0} attacks {1}", this, unitToAttack));
        }

        private void ReceiveDamage(int damage)
        {
            this.CurrentHitPoints -= Math.Max(0, damage - DamageReduction);
        }

        private Unit ChooseUnit(IList<Unit> units)
        {
            var livingUnits = units.Where(unit => unit.IsAlive).ToList();

            var advantageUnits = livingUnits.Where(unit => UnitClass.GetDifficulty(unit.UnitClass) == Difficulty.Advantage).ToList();

            if (advantageUnits.Any())
            {
                return advantageUnits.Random();
            }

            var equalUnits = livingUnits.Where(unit => UnitClass.GetDifficulty(unit.UnitClass) == Difficulty.Equal).ToList();

            return equalUnits.Any() ? equalUnits.Random() : livingUnits.Random();
        }
    }
}