using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Battle
    {
        public static void Run(IEnumerable<Unit> heroes, IEnumerable<Unit> foes, bool isSurpriseBattle)
        {
            Debug.Log("A battle starts!");

            var heroList = heroes.ToList();
            var foeList = foes.ToList();

            if (isSurpriseBattle)
            {
                foreach (var foe in foeList.Where(foe => foe.IsAlive))
                {
                    foe.Attack(heroList);
                }
            }

            while (foeList.Any(foe => foe.IsAlive) && heroList.Any(hero => hero.IsAlive))
            {
                foreach (var hero in heroList.Where(hero => hero.IsAlive))
                {
                    if (foeList.Any(foe => foe.IsAlive))
                    {
                        hero.Attack(foeList);
                    }
                }

                foreach (var foe in foeList.Where(foe => foe.IsAlive))
                {
                    if (heroList.Any(hero => hero.IsAlive))
                    {
                        foe.Attack(heroList);
                    }
                }
            }
            
            Debug.Log("The battle finished!");
        }
    }
}