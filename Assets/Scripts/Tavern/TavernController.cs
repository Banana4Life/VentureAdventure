using System.Collections.Generic;
using Model;
using Model.UnitClasses;
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

        public GameObject MoneyText;
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
            var stats = listItem.transform.GetChild(1);
            stats.GetChild(1).gameObject.GetComponent<Text>().text = adventurer.Name;
            stats.GetChild(3).gameObject.GetComponent<Text>().text = adventurer.Level.ToString();
            stats.GetChild(5).gameObject.GetComponent<Text>().text = adventurer.UnitClass.UnitType.ToString();
            UpdateIndex(investmentList, index);
            RecalcInvestmentAndStake(investmentList, index);
        }

        private int _money = 100; // TODO: remove

        public void ToggleEquipment(Equipment equipment, bool value, int index)
        {
            var adventurer = _investableAdventurers[index];
            var weapon = equipment as Weapon;
            var armor = equipment as Armor;
            if (weapon != null)
            {
                adventurer.Weapon = value ? weapon : null;
            }
            else if (armor != null)
            {
                adventurer.Armor = value ? armor : null;
            }

            RecalcInvestmentAndStake(true, index);
            if (adventurer.GetEquipmentWorth() > _money)
            {
                InvestmentPanelList.transform.GetChild(index).GetChild(5).GetChild(1).gameObject.GetComponent<Text>().color = Color.red;
            }
            else if (adventurer.GetEquipmentWorth() <= _money)
            {
                InvestmentPanelList.transform.GetChild(index).GetChild(5).GetChild(1).gameObject.GetComponent<Text>().color = Color.black;
            }
        }

        private static int GetStake(int level, int equipmentWorth)
        {
            return 10;
        }

        private void RecalcInvestmentAndStake(bool investmentList, int index)
        {
            var adventurers = investmentList ? _investableAdventurers : _investedAdventurers;
            var list = investmentList ? InvestmentPanelList : InvestedPanelList;
            var adventurer = adventurers[index];
            var investmentStats = list.transform.GetChild(index).GetChild(5);
            investmentStats.GetChild(1).GetComponent<Text>().text = adventurer.GetEquipmentWorth() +"$";
            investmentStats.GetChild(3).GetComponent<Text>().text =
                GetStake(adventurer.Level, adventurer.GetEquipmentWorth()) + "%";
        }

        public void Invest(int index)
        {
            Debug.Log("invest: " + index);
            var adventurer = _investableAdventurers[index];
            if (adventurer.GetEquipmentWorth() <= _money)
            {
                _money -= adventurer.GetEquipmentWorth();
                _investedAdventurers.Add(adventurer);
                _investableAdventurers.RemoveAt(index);
                DestroyImmediate(InvestmentPanelList.transform.GetChild(index).gameObject);
                UpdateIndices(true);
                AddToInvested(adventurer);
                UpdateMoney();
            }
            else
            {
                Debug.LogWarning("Tried to invest without enough money");
            }

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

        public void UpdateIndex(bool investmentList, int index)
        {
            var list = investmentList ? InvestmentPanelList : InvestedPanelList;
            var listItem = list.transform.GetChild(index);
            Debug.Log("update: " + index);
            var button = listItem.GetChild(2).gameObject.GetComponent<Button>();
            var toggleArmor = listItem.GetChild(3).gameObject.GetComponent<Toggle>();
            var toggleWeapon = listItem.GetChild(4).gameObject.GetComponent<Toggle>();

            button.onClick.RemoveAllListeners();
            if (investmentList)
            {
                button.onClick.AddListener(() => Invest(index));
            }
            else
            {
                button.onClick.AddListener(() => AddToParty(index));
            }


            if (investmentList)
            {
                toggleArmor.onValueChanged.RemoveAllListeners();
                toggleArmor.onValueChanged.AddListener(value => ToggleEquipment(new ChainmailArmor(), value, index));
                toggleWeapon.onValueChanged.RemoveAllListeners();
                toggleWeapon.onValueChanged.AddListener(value => ToggleEquipment(new Sword(), value, index));
            }
        }

        public void UpdateIndices(bool investmentList)
        {
            var list = investmentList ? InvestmentPanelList : InvestedPanelList;
            for (var i = 0; i < list.transform.childCount; i++)
            {
                UpdateIndex(investmentList, i);
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

        private void UpdateMoney()
        {
            MoneyText.GetComponent<Text>().text = _money + " $";
        }

        public void OnEnable()
        {
            UpdateMoney();
        }
    }
}