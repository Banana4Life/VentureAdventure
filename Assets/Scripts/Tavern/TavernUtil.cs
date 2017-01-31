using System;
using Model;
using Model.UnitClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Tavern
{
    public class TavernUtil
    {
        private static readonly string[] MALE_FORENAMES = {"Wolfram", "Huldbrand", "Alberto", "Dolfus", "Bertram", "Waldemar", "Jonas", "Phillip", "Ruben", "Ayu", "Florian", "Dominik", "Henry", "Hiro", "Max", "Rolf", "Richard", "Kenny"};
        private static readonly string[] LASTNAMES = {"Weinberger", "Goldstein", "Stahlfaust", "Orkenhauer", "Siebenschläfer", "Sonnstern", "Morgentau", "Feenfummler", "Nixen", "Silbereisen", "Messerschmitt", "Wettrüster", "Cheater", "Horder", "Buff"};
        private static readonly string[] FEMALE_FORENAMES = {"Hildegard", "Berthalda", "Henrietta", "Valeria", "Lilia", "Sherry", "Anna", "Kimchi", "Rosalinda", "Luise", "Loreley", "Luna", "Mia", "Pia"};

        private static readonly Sprite MaleFighter = Resources.Load<Sprite>("2-a");
        private static readonly Sprite FemaleFighter = Resources.Load<Sprite>("1-a");
        private static readonly Sprite MaleRanger = Resources.Load<Sprite>("6-a");
        private static readonly Sprite FemaleRanger = Resources.Load<Sprite>("5-a");
        private static readonly Sprite MalePriest = Resources.Load<Sprite>("4-a");
        private static readonly Sprite FemalePriest = Resources.Load<Sprite>("3-a");

        public static Unit generateNewRandomAdventurer()
        {
            var result = new Unit();
            result.GrantExperience(0);
            result.Male = Random.Range(0, 2) == 0;
            string[] forenames = result.Male ? MALE_FORENAMES : FEMALE_FORENAMES;
            var forename = forenames[Random.Range(0, forenames.Length)];
            var lastname = LASTNAMES[Random.Range(0, LASTNAMES.Length)];
            result.Name = (result.Male ? "Sir " : "Madame ") + forename + " " + lastname;
            var unitClassIndex = Random.Range(0, 3);
            UnitClass unitClass;
            switch (unitClassIndex)
            {
                case 0:
                    unitClass = new FighterClass();
                    break;
                case 1:
                    unitClass = new RangerClass();
                    break;
                case 2:
                    unitClass = new PriestClass();
                    break;
                default:
                    unitClass = new FighterClass();
                    break;
            }
            result.UnitClass = unitClass;
            return result;
        }

        public static int getAdventurerWorth(Unit adventurer)
        {
            return (int) Mathf.Round(Mathf.Pow(adventurer.Level, 1.2f) * 25 + adventurer.GetEquipmentWorth());
        }

        public static int getStake(Unit adventurer)
        {
            return (int) Mathf.Round(getAdventurerWorth(adventurer) / (Mathf.Pow(adventurer.Level, 1.2f) * 3));
        }

        public static Sprite getPortrait(Unit adventurer)
        {
            switch (adventurer.UnitClass.UnitType)
            {
                case UnitType.Fighter:
                    return adventurer.Male ? MaleFighter : FemaleFighter;
                case UnitType.Ranger:
                    return adventurer.Male ? MaleRanger : FemaleRanger;
                case UnitType.Priest:
                    return adventurer.Male ? MalePriest : FemalePriest;
                default:
                    Debug.LogError("Tried to get portrait for non existant human class");
                    return null;
            }
        }

        public static GameState getGameState()
        {
            var rootGos = SceneManager.GetSceneByName("Map").GetRootGameObjects();
            var worldGraph = rootGos[0];
            foreach (var go in rootGos)
            {
                if (go.name != "GameRoot")
                    continue;
                worldGraph = go;
                break;
            }
            var worldLoopManager = worldGraph.GetComponent<WorldLoopManager>();
            return worldLoopManager.GameState;
        }
    }
}