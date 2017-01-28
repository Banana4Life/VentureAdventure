using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Battle
    {
        public void Run(IList<Unit> heroes, IList<Unit> foes, bool isSurpriseBattle)
        {
            if (isSurpriseBattle)
            {
                foreach (var foe in foes.Where(foe => foe.IsAlive))
                {
                    foe.Attack(heroes);
                }
            }

            while (foes.Any(foe => foe.IsAlive) && heroes.Any(hero => hero.IsAlive))
            {
                foreach (var hero in heroes.Where(hero => hero.IsAlive))
                {
                    hero.Attack(foes);
                }

                foreach (var foe in foes.Where(foe => foe.IsAlive))
                {
                    foe.Attack(heroes);
                }
            }
        }
    }

    public class Unit
    {
        public int Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public UnitClass UnitClass { get; set; }
        public int CurrentHitPoints { get; set; }
        public bool IsAlive { get { return this.CurrentHitPoints > 0; } }

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
        
        public void Attack(IList<Unit> units)
        {
            var unitToAttack = ChooseUnit(units);
            unitToAttack.ReceiveDamage(Damage);
        }

        private void ReceiveDamage(int damage)
        {
            this.CurrentHitPoints -= Math.Max(0, damage - DamageReduction);
        }

        private Unit ChooseUnit(IList<Unit> units)
        {
            var livingUnits = units.Where(unit => unit.IsAlive).ToList();

            var advantageUnits = livingUnits
                .Where(unit => UnitClass.GetDifficulty(unit.UnitClass) == Difficulty.Advantage)
                .ToList();

            if (advantageUnits.Any())
            {
                return advantageUnits.Random();
            }

            var equalUnits = livingUnits
                .Where(unit => UnitClass.GetDifficulty(unit.UnitClass) == Difficulty.Equal)
                .ToList();

            return equalUnits.Any() 
                ? equalUnits.Random() 
                : livingUnits.Random();
        }
    }
}