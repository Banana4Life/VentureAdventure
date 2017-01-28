using System.Collections.Generic;
using Model;
using Model.UnitClasses;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    private List<Unit> _investedAdventurers;

    public TavernController()
    {
        _investableAdventurers = new List<Unit>();
        _investedAdventurers = new List<Unit>();
    }

    public void OpenInvestmentPanel()
    {
        var count = _investableAdventurers.Count;
        for (var i = count; i < count + 5; i++)
        {
            var adventurer = GenerateNewRandomAdventurer();
            _investableAdventurers.Add(adventurer);
            var listItem = Instantiate(AdventurerListItemPrefab);
            FillAdventuterListItem(listItem, adventurer, i);
            listItem.transform.parent = InvestmentPanelList.transform;
            listItem.transform.localScale = Vector3.one;
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

    private void FillAdventuterListItem(GameObject listItem, Unit adventurer, int index)
    {
        Sprite portrait;
        switch (adventurer.UnitClass.UnitType)
        {
            case UnitType.Fighter:
                portrait = adventurer.Male ? MaleFighter : FemaleFighter;
                break;
            case UnitType.Ranger:
                portrait = adventurer.Male ? MaleRanger : FemaleRanger;
                break;
            case UnitType.Priest:
                portrait = adventurer.Male ? MalePriest : FemalePriest;
                break;
            default:
                portrait = MaleFighter;
                break;
        }
        listItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = portrait;
        listItem.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Invest(index));
        var stats = listItem.transform.GetChild(1);
        stats.GetChild(1).gameObject.GetComponent<Text>().text = adventurer.Name;
        stats.GetChild(3).gameObject.GetComponent<Text>().text = adventurer.Level.ToString();
        stats.GetChild(5).gameObject.GetComponent<Text>().text = adventurer.UnitClass.UnitType.ToString();
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
        var listItem = Instantiate(InvestedAdventurerListItemPrefab);
        var index = _investedAdventurers.Count - 1;
        FillAdventuterListItem(listItem, _investedAdventurers[index], index);
        listItem.transform.parent = InvestedPanelList.transform;
        listItem.transform.localScale = Vector3.one;
    }
}