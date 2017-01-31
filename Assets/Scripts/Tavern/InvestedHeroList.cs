using Model;
using Model.Equipment.Armors;
using Model.Equipment.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern
{
    public class InvestedHeroList : HeroList
    {
        public void ActivateAdventurer(int index)
        {
            gameObject.transform.GetChild(index).GetChild(2).gameObject.GetComponent<Button>().interactable = true;
        }

        public void DeactivateAdventurer(int index)
        {
            gameObject.transform.GetChild(index).GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        }

        protected override void FillAdventurerListItem(Unit adventurer, int index)
        {
            GameObject listItem;
            listItem = Instantiate(ListItemPrefab);
            listItem.transform.SetParent(gameObject.transform);
            listItem.transform.localScale = Vector3.one;
            listItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TavernUtil.getPortrait(adventurer);

            listItem.transform.GetChild(3).gameObject.GetComponent<Toggle>().interactable = false;
            listItem.transform.GetChild(3).gameObject.GetComponent<Toggle>().isOn = adventurer.Armor is ChainmailArmor;
            listItem.transform.GetChild(4).gameObject.GetComponent<Toggle>().interactable = false;
            listItem.transform.GetChild(4).gameObject.GetComponent<Toggle>().isOn = adventurer.Weapon is Sword;

            UpdateLevelAndBadge(index);
            UpdateIndex(index);
            RecalcInvestmentAndStake(index);
        }

        public override void UpdateIndex(int index)
        {
            var listItem = gameObject.transform.GetChild(index);
            Debug.Log("update: " + index);
            var button = listItem.GetChild(2).gameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() =>
            {
                GameObject.Find("ClickSound1").GetComponent<AudioSource>().Play();
                TavernControllerGo.GetComponent<TavernController>().AddToParty(index);
            });
        }
    }
}