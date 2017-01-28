﻿using System.Collections.Generic;
using Model;
using Model.UnitClasses;
using UnityEngine;
using UnityEngine.UI;

public class TavernController : MonoBehaviour
{
    public GameObject InvestmentPanel;
    public GameObject InvestmentPanelList;
    public GameObject AdventurerListItemPrefab;

    private List<Unit> InvestableAdventurers = new List<Unit>();

    public void OpenInvestmentPanel()
    {
        InvestmentPanel.SetActive(true);
        InvestableAdventurers.Clear();
        for (var i = 0; i < 6; i++)
        {
            var adventurer = GenerateNewRandomAdventurer();
            InvestableAdventurers.Add(adventurer);
            var listItem = Instantiate(AdventurerListItemPrefab);
            FillAdventuterListItem(listItem, adventurer);
            listItem.transform.parent = InvestmentPanelList.transform;
            listItem.transform.localScale = Vector3.one;
        }
    }

    public void CloseInvestmentPanel()
    {
        InvestmentPanel.SetActive(false);
    }

    private static Unit GenerateNewRandomAdventurer()
    {
        var result = new Unit();
        result.GrantExperience(0);
        result.Name = "Sir Test";
        result.UnitClass = new FighterClass();
        return result;
    }

    private static void FillAdventuterListItem(GameObject listItem, Unit adventurer)
    {
        listItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = adventurer.Name;
        listItem.transform.GetChild(5).gameObject.GetComponent<Text>().text = adventurer.Level.ToString();
        listItem.transform.GetChild(7).gameObject.GetComponent<Text>().text =
            adventurer.UnitClass.UnitType.ToString();
    }
}