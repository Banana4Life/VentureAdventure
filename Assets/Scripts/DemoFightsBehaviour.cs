using System.Collections.Generic;
using Model;
using Model.Armors;
using Model.UnitClasses;
using Model.Weapons;
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


        var lastLevel = 0;
        while (orc1.Level < 10)
        {
            if (orc1.Level > lastLevel)
            {
                Debug.Log(orc1);
                lastLevel = orc1.Level;
            }

            orc1.GrantExperience(200);
        }

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