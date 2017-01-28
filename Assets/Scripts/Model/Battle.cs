using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class Battle
    {
        public static void Run(IList<Unit> heroes, IList<Unit> foes, bool isSurpriseBattle)
        {
            Debug.Log("A battle starts!");

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
                    if (foes.Any(foe => foe.IsAlive))
                    {
                        hero.Attack(foes);
                    }
                }

                foreach (var foe in foes.Where(foe => foe.IsAlive))
                {
                    if (heroes.Any(hero => hero.IsAlive))
                    {
                        foe.Attack(heroes);
                    }
                }
            }
            
            Debug.Log("The battle finished!");
        }
    }
}