using System.Collections.Generic;
using Model;
using Tavern;
using UnityEngine;
using UnityEngine.UI;

public class Party : MonoBehaviour
{
    public GameObject TavernControllerGo;
    public Sprite PartySlotBackground;

    private TavernController tavernController;

    public int Count {
        get { return _party.Count; }
    }

    private readonly List<Unit> _party;
    private readonly List<int> _partyIndices;

    public bool adventuring { get; private set; }

    public Party()
    {
        _party = new List<Unit>();
        _partyIndices = new List<int>();
        adventuring = false;
    }

    public void OnEnable()
    {
        tavernController = TavernControllerGo.GetComponent<TavernController>();
    }

    public void Add(Unit adventurer, int index)
    {
        if (_party.Count == 0)
        {
            gameObject.SetActive(true);
        }
        else if (_party.Count > 2)
        {
            Debug.LogWarning("Tried to add more than three adventurers to a party");
            return;
        }

        Debug.Log("add to party in slot: " + index);
        var slot = gameObject.transform.GetChild(_party.Count + 1).gameObject;
        slot.transform.GetChild(0).GetComponent<Image>().sprite = TavernUtil.getPortrait(adventurer);
        slot.transform.GetChild(1).GetComponent<Text>().text = adventurer.Name;
        slot.transform.GetChild(2).gameObject.SetActive(true);
        slot.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameObject.Find("ClickSound1").GetComponent<AudioSource>().Play();
            tavernController.RemoveFromParty(_party.Count - 1);
        });


        _party.Add(adventurer);
        _partyIndices.Add(index);
    }

    public Unit Get(int index)
    {
        return _party[index];
    }

    public Model.Party GetParty()
    {
        var gameState = TavernUtil.getGameState();
        var party = new Model.Party {CurrentNode = gameState.WorldGraph.TavernNode};
        foreach (var unit in _party)
        {
            party.AddMember(unit);
        }
        return party;
    }

    public void RemoveAt(int partySlot)
    {
        Debug.Log("remove from party: " + partySlot);
        if (partySlot >= _party.Count)
        {
            Debug.LogWarning("Tried to remove a non existent party member");
            return;
        }
        var remove = partySlot;
        if (partySlot < _party.Count - 1)
        {
            remove = _party.Count - 1;
            var newSlot = gameObject.transform.GetChild(partySlot + 1).gameObject;
            var oldSlot = gameObject.transform.GetChild(_party.Count).gameObject;
            newSlot.transform.GetChild(0).GetComponent<Image>().sprite = oldSlot.transform.GetChild(0).GetComponent<Image>().sprite;
            newSlot.transform.GetChild(1).GetComponent<Text>().text = oldSlot.transform.GetChild(1).GetComponent<Text>().text;
            newSlot.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            newSlot.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => tavernController.RemoveFromParty(_party.Count - 1));
        }
        var slot = gameObject.transform.GetChild(remove + 1).gameObject;
        slot.transform.GetChild(0).GetComponent<Image>().sprite = PartySlotBackground;
        slot.transform.GetChild(1).GetComponent<Text>().text = "";
        slot.transform.GetChild(2).gameObject.SetActive(false);
        slot.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        _party.RemoveAt(partySlot);

        _partyIndices.RemoveAt(partySlot);

        if (partySlot == 0 && _party.Count == 2)
        {
            var character = _party[0];
            _party.RemoveAt(0);
            _party.Add(character);
        }

        if (_party.Count == 0)
        {
            gameObject.SetActive(false);
            ReturnHome();
        }
    }

    public void Clear()
    {
        var count = _party.Count;
        for (var i = count - 1; i >= 0; i--)
        {
            RemoveAt(i);
        }
    }

    public int GetIndex(int partySlot)
    {
        return _partyIndices[partySlot];
    }

    public void SetIndex(int partySlot, int index)
    {
        _partyIndices[partySlot] = index;
    }

    public void StartAdventuring()
    {
        adventuring = true;
        var buttons = gameObject.GetComponentsInChildren<Button>();
        buttons[0].interactable = false;
        buttons[0].transform.GetChild(0).gameObject.GetComponent<Text>().text = "Adventuring...";
        for (var i = 1; i < _party.Count + 1; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void ReturnHome()
    {
        adventuring = false;
        var button = gameObject.GetComponentInChildren<Button>();
        button.interactable = true;
        button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Send party";
        Clear();
    }
}
