using System.Collections.Generic;
using Model;
using Model.UnitClasses;
using UnityEngine;

public class DemoFightsBehaviour : MonoBehaviour
{
    public void Start()
    {
        var heroes = new List<Unit>
        {
            new Unit
            {
                Name = "Conan Black",
                UnitClass = new FighterClass(),
                Armor = new ShirtArmor(),
                Weapon = new StickWeapon(),
            },
            new Unit
            {
                Name = "Bruce Cruise",
                UnitClass = new FighterClass(),
                Armor = new ShirtArmor(),
                Weapon = new StickWeapon()
            }
        };

        var orc1 = new Unit
        {
            Name = "Hagthorn Ethwrak",
            UnitClass = new OrcClass(),
            Armor = new ShirtArmor(),
            Weapon = new StickWeapon(),
        };

        orc1.GrantExperience(500);

        var foes = new List<Unit>
        {
            orc1
        };


        Battle.Run(heroes, foes, false);

        foreach (var hero in heroes)
        {
            Debug.Log(hero);
        }
    }
}

public class StickWeapon : Weapon
{
    protected override int Damage
    {
        get { return 1; }
    }
}

public class ShirtArmor : Armor
{
    protected override int DamageReduction
    {
        get { return 0; }
    }
}


