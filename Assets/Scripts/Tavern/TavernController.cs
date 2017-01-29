using System.Collections.Generic;
using Model;
using Model.Armors;
using Model.UnitClasses;
using Model.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using World;
using Random = UnityEngine.Random;

namespace Tavern
{
    public class TavernController : MonoBehaviour
    {
        private static readonly string[] MaleForenames = {"Herbert", "Bert", "Robert", "Heidelbert", "Karlsbert", "Bertbert"};
        private static readonly string[] MaleLastnames = {"Schachtel", "Damm", "Meier", "Maier", "Mayer", "Meyer"};
        private static readonly string[] FemaleForenames = {"Herbertine", "Bertha", "Robertine", "Heidelbertine", "Karlsbertine", "Bertbertha"};
        private static readonly string[] FemaleLastnames = {"Schachtel", "Damm", "Meier", "Maier", "Mayer", "Meyer"};

        public GameObject InvestmentPanel;
        public GameObject InvestmentPanelList;
        public GameObject InvestedPanelList;
        public GameObject AdventurerListItemPrefab;
        public GameObject InvestedAdventurerListItemPrefab;
        public GameObject Party;
        public Sprite MaleFighter;
        public Sprite FemaleFighter;
        public Sprite MaleRanger;
        public Sprite FemaleRanger;
        public Sprite MalePriest;
        public Sprite FemalePriest;

        private readonly List<Unit> _investableAdventurers;
        private readonly List<Unit> _investedAdventurers;
        private readonly List<Unit> _party;

        public TavernController()
        {
            _investableAdventurers = new List<Unit>();
            _investedAdventurers = new List<Unit>();
            _party = new List<Unit>();
        }

        public void OpenInvestmentPanel()
        {
            var count = _investableAdventurers.Count;
            for (var i = count; i < count + 5; i++)
            {
                var adventurer = GenerateNewRandomAdventurer();
                _investableAdventurers.Add(adventurer);
                FillAdventurerListItem(true, adventurer, i);
            }
            InvestmentPanel.SetActive(true);
        }

        public void CloseInvestmentPanel()
        {
            InvestmentPanel.SetActive(false);
        }

        private static Unit GenerateNewRandomAdventurer()
        {
            var result = new Unit();
            result.GrantExperience(0);
            result.Male = Random.Range(0, 2) == 0;
            string[] forenames;
            string[] lastnames;
            if (result.Male)
            {
                forenames = MaleForenames;
                lastnames = MaleLastnames;
            }
            else
            {
                forenames = FemaleForenames;
                lastnames = FemaleLastnames;
            }
            var forename = forenames[Random.Range(0, 6)];
            var lastname = lastnames[Random.Range(0, 6)];
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

        private void FillAdventurerListItem(bool investmentList, Unit adventurer, int index)
        {
            GameObject listItem;
            if (investmentList)
            {
                listItem = Instantiate(AdventurerListItemPrefab);
                listItem.transform.parent = InvestmentPanelList.transform;
            }
            else
            {
                listItem = Instantiate(InvestedAdventurerListItemPrefab);
                listItem.transform.parent = InvestedPanelList.transform;
            }
            listItem.transform.localScale = Vector3.one;
            listItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GetPortrait(adventurer.UnitClass, adventurer.Male);
            if (investmentList)
            {
                listItem.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Invest(index));
            }
            else
            {
                listItem.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => AddToParty(index));
            }
            var stats = listItem.transform.GetChild(1);
            stats.GetChild(1).gameObject.GetComponent<Text>().text = adventurer.Name;
            stats.GetChild(3).gameObject.GetComponent<Text>().text = adventurer.Level.ToString();
            stats.GetChild(5).gameObject.GetComponent<Text>().text = adventurer.UnitClass.UnitType.ToString();
            if (investmentList)
            {
                listItem.transform.GetChild(3).gameObject.GetComponent<Toggle>().onValueChanged.AddListener(value => ToggleArmor(value, index));
                listItem.transform.GetChild(4).gameObject.GetComponent<Toggle>().onValueChanged.AddListener(value => ToggleWeapon(value, index));
            }
            RecalcInvestmentAndStake(index);
        }

        public void ToggleArmor(bool value, int index)
        {
            var adventurer = _investableAdventurers[index];
            adventurer.Armor = value ? new ChainmailArmor() : null;
            RecalcInvestmentAndStake(index);
        }

        public void ToggleWeapon(bool value, int index)
        {
            var adventurer = _investableAdventurers[index];
            adventurer.Weapon = value ? new Sword() : null;
        RecalcInvestmentAndStake(index);
        }

        private static int GetStake(int level, int equipmentWorth)
        {
            return 10;
        }

        private void RecalcInvestmentAndStake(int index)
        {
            var adventurer = _investableAdventurers[index];
            var investmentStats = InvestmentPanelList.transform.GetChild(index).GetChild(5);
            investmentStats.GetChild(1).GetComponent<Text>().text = adventurer.GetEquipmentWorth() +"$";
            investmentStats.GetChild(3).GetComponent<Text>().text =
                GetStake(adventurer.Level, adventurer.GetEquipmentWorth()) + "%";
        }

        public void Invest(int index)
        {
            Debug.Log("invest: " + index);
            var adventurer = _investableAdventurers[index];
            _investedAdventurers.Add(adventurer);
            _investableAdventurers.RemoveAt(index);
            DestroyImmediate(InvestmentPanelList.transform.GetChild(index).gameObject);
            UpdateIndices();
            AddToInvested(adventurer);
        }

        public void AddToParty(int index)
        {
            var adventurer = _investedAdventurers[index];
            if (_party.Count == 0)
            {
                Party.SetActive(true);
            }
            else if (_party.Count > 2)
            {
                Debug.LogWarning("Tried to add more than three adventurers to a party");
                return;
            }

            Debug.Log("add to party in slot: " + index);
            var slot = Party.transform.GetChild(_party.Count + 1).gameObject;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = GetPortrait(adventurer.UnitClass, adventurer.Male);
            slot.transform.GetChild(1).GetComponent<Text>().text = adventurer.Name;
            slot.transform.GetChild(2).gameObject.SetActive(true);

            _party.Add(adventurer);
        }

        public void RemoveFromParty(int index)
        {
            Debug.Log("remove from party: " + index);
            if (index >= _party.Count)
            {
                Debug.LogWarning("Tried to remove a non existent party member");
                return;
            }
            var remove = index;
            if (index < _party.Count - 1)
            {
                remove = _party.Count - 1;
                var newSlot = Party.transform.GetChild(index + 1).gameObject;
                var oldSlot = Party.transform.GetChild(_party.Count).gameObject;
                newSlot.transform.GetChild(0).GetComponent<Image>().sprite = oldSlot.transform.GetChild(0).GetComponent<Image>().sprite;
                newSlot.transform.GetChild(1).GetComponent<Text>().text = oldSlot.transform.GetChild(1).GetComponent<Text>().text;
            }
            var slot = Party.transform.GetChild(remove + 1).gameObject;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot.transform.GetChild(1).GetComponent<Text>().text = "";
            slot.transform.GetChild(2).gameObject.SetActive(false);

            _party.RemoveAt(index);

            if (index == 0 && _party.Count == 2)
            {
                var character = _party[0];
                _party.RemoveAt(0);
                _party.Add(character);
            }

            if (_party.Count == 0)
            {
                Party.SetActive(false);
            }
        }

        public void ClearParty()
        {
            var count = _party.Count;
            for (var i = 0; i < count; i++)
            {
                RemoveFromParty(i);
            }
        }

        public void UpdateIndices()
        {
            for (var i = 0; i < InvestmentPanelList.transform.childCount; i++)
            {
                var index = i;
                Debug.Log("update: " + index);
                var button = InvestmentPanelList.transform.GetChild(index).GetChild(2).gameObject.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Invest(index));
            }
        }

        public void AddToInvested(Unit adventurer)
        {
            var index = _investedAdventurers.Count - 1;
            FillAdventurerListItem(false, _investedAdventurers[index], index);
        }

        private Sprite GetPortrait(UnitClass adventurerClass, bool male)
        {
            switch (adventurerClass.UnitType)
            {
                case UnitType.Fighter:
                    return male ? MaleFighter : FemaleFighter;
                case UnitType.Ranger:
                    return male ? MaleRanger : FemaleRanger;
                case UnitType.Priest:
                    return male ? MalePriest : FemalePriest;
                default:
                    Debug.LogError("Tried to get portrait for non existant human class");
                    return null;
            }
        }

        public void HideTavern()
        {
            GameObject.Find("TavernCanvas").SetActive(false);
            GameObject.Find("TavernMusic").GetComponent<AudioSource>().mute = true;
        }

        private float _squishyScale;
        private int _squishyDir = 1;
        private static readonly float SquishySteps = 40f;
        private void Update()
        {
            if (_squishyScale <= 0f)
            {
                _squishyDir = 1;
            }
            else if (_squishyScale >= 1f)
            {
                _squishyDir = -1;
            }
            var count = InvestmentPanelList.transform.childCount;
            for (var i = 0; i < count; i++)
            {
                var rectTransform = InvestmentPanelList.transform.GetChild(i)
                    .GetChild(0)
                    .gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(160, 200 - Mathf.Lerp(0f, 8f, _squishyScale));
            }
            count = InvestedPanelList.transform.childCount;
            for (var i = 0; i < count; i++)
            {
                var rectTransform = InvestedPanelList.transform.GetChild(i)
                    .GetChild(0)
                    .gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(160, 200 - Mathf.Lerp(0f, 8f, _squishyScale));
            }
            _squishyScale += 1f / SquishySteps * _squishyDir;
        }

        public void SendParty()
        {
            var rootGos = SceneManager.GetSceneByName("Map").GetRootGameObjects();
            var worldGraph = rootGos[0];
            foreach (var go in rootGos)
            {
                if (go.name != "WorldGraph")
                    continue;
                worldGraph = go;
                break;
            }
            var worldGraphController = worldGraph.GetComponent<WorldGraphController>();
            var heroController = worldGraph.GetComponent<HeroController>();
            Debug.Log(rootGos);
            Debug.Log(rootGos[1]);
            Debug.Log(heroController);
            Debug.Log(worldGraphController);
            heroController.SpawnHeros(_party, worldGraphController);
            HideTavern();
            ClearParty();
        }
    }
}