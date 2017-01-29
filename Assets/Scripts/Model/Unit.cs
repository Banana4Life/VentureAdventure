using System;
using System.Collections.Generic;
using System.Linq;
using Model.Equipment;
using Model.Util;
using Model.World;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Model
{
    public class Unit
    {
        private readonly HashSet<Connection> _knownConnections = new HashSet<Connection>();

        private ArmorBase _armor = new NoArmor();
        private WeaponBase _weapon = new NoWeapon();

        public string Name { get; set; }
        public UnitClass UnitClass { get; set; }
        public int Experience { get; private set; }
        public int CurrentHitPoints { get; private set; }
        public bool IsAlive { get { return CurrentHitPoints > 0; } }

        public HashSet<Connection> KnownConnections
        {
            get { return _knownConnections; }
        }

        public bool Male { get; set; }

        public ArmorBase Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        public WeaponBase Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        public Unit()
        {
            CurrentHitPoints = MaxHitPoints;
        }

        public int Level
        {
            get
            {
                if (Experience < ExperienceLevels.Level02) return 1;
                if (Experience < ExperienceLevels.Level03) return 2;
                if (Experience < ExperienceLevels.Level04) return 3;
                if (Experience < ExperienceLevels.Level05) return 4;
                if (Experience < ExperienceLevels.Level06) return 5;
                if (Experience < ExperienceLevels.Level07) return 6;
                if (Experience < ExperienceLevels.Level08) return 7;
                if (Experience < ExperienceLevels.Level09) return 8;
                return Experience < ExperienceLevels.Level10 ? 9 : 10;    
            }
        }
        
        public int MaxHitPoints
        {
            get { return GameData.BaseHitPoints + Level * Mathf.CeilToInt(Mathf.Pow(GameData.HitPointsExponent, Level) * GameData.HitPointsPerLevel); }
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

            var hpBefore = unitToAttack.CurrentHitPoints;


            Debug.Log(string.Format("{0} attacks {1}", this, unitToAttack));

            int damage;
            switch (difficulty)
            {
                case Difficulty.Advantage:
                    damage = Mathf.CeilToInt(Mathf.Lerp(Damage*0.75f, Damage*1.25f, UnityEngine.Random.value));
                    break;
                case Difficulty.Disadvantage:
                    damage = Mathf.CeilToInt(Mathf.Lerp(Damage * 0.25f, Damage * 0.75f, UnityEngine.Random.value));
                    break;
                default:
                    damage = Mathf.CeilToInt(Mathf.Lerp(Damage*0.5f, Damage, UnityEngine.Random.value));
                    break;
            }

            unitToAttack.ReceiveDamage(damage);

            Debug.Log(string.Format("{0}'s attack dealt {1}HP Damage.", this, hpBefore - unitToAttack.CurrentHitPoints));
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
        
        public override string ToString()
        {
            return string.Format("{0} [{1} Lvl.{4} - {2}/{3}]", Name, UnitClass.UnitType, CurrentHitPoints, MaxHitPoints, Level);
        }

        public int GetEquipmentWorth()
        {
            var worth = 0;
            if (Armor != null)
            {
                worth += Armor.Cost;
            }
            if (Weapon != null)
            {
                worth += Weapon.Cost;
            }
            return worth;
        }

        public void RegenerateHitPoints()
        {
            this.CurrentHitPoints = this.MaxHitPoints;
        }
    }
}