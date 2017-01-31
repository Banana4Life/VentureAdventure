using System.Collections.Generic;
using Model;
using Tavern;
using UnityEngine;
using UnityEngine.UI;

public abstract class HeroList : MonoBehaviour
{
    public GameObject TavernControllerGo;
    public GameObject ListItemPrefab;

    public int Count {
        get { return heroList.Count; }
    }

    protected List<Unit> heroList;

    public HeroList()
    {
        heroList = new List<Unit>();
    }

    public void Add(Unit adventurer)
    {
        heroList.Add(adventurer);
        FillAdventurerListItem(adventurer, heroList.Count - 1);
    }

    public void RemoveAt(int index)
    {
        heroList.RemoveAt(index);
        DestroyImmediate(gameObject.transform.GetChild(index).gameObject);
        UpdateIndices();
    }

    public Unit Get(int index)
    {
        return heroList[index];
    }

    protected abstract void FillAdventurerListItem(Unit adventurer, int index);

    protected void RecalcInvestmentAndStake(int index)
    {
        var adventurer = heroList[index];
        var investmentStats = gameObject.transform.GetChild(index).GetChild(5);
        investmentStats.GetChild(1).GetComponent<Text>().text = adventurer.Level * 25 + adventurer.GetEquipmentWorth() +"$";
        investmentStats.GetChild(3).GetComponent<Text>().text =
            TavernUtil.getStake(adventurer) + "%";
    }

    public abstract void UpdateIndex(int index);

    public void UpdateIndices()
    {
        for (var i = 0; i < gameObject.transform.childCount; i++)
        {
            UpdateIndex(i);
        }
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
        var count = gameObject.transform.childCount;
        for (var i = 0; i < count; i++)
        {
            var rectTransform = gameObject.transform.GetChild(i)
                .GetChild(0)
                .gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(160, 200 - Mathf.Lerp(0f, 8f, _squishyScale));
        }
        _squishyScale += 1f / SquishySteps * _squishyDir;
    }
}
