using System;
using Model;
using Model.Equipment;
using Model.Equipment.Armors;
using Model.Equipment.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern
{
    public class InvestableHeroList : HeroList
    {
        public void CheckBuyable()
        {
            var count = heroList.Count;
            for (var i = 0; i < count; i++)
            {
                var adventurer = heroList[i];
                if (TavernUtil.getAdventurerWorth(adventurer) > TavernUtil.getGameState().Money)
                {
                    gameObject.transform.GetChild(i).GetChild(5).GetChild(1).gameObject.GetComponent<Text>().color = Color.red;
                    gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Button>().interactable = false;
                }
                else if (TavernUtil.getAdventurerWorth(adventurer) <= TavernUtil.getGameState().Money)
                {
                    gameObject.transform.GetChild(i).GetChild(5).GetChild(1).gameObject.GetComponent<Text>().color = Color.black;
                    gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }

        public void ToggleEquipment(EquipmentBase equipment, bool value, int index)
        {
            Debug.Log("toggle equip: " + value + " " + index);
            var adventurer = heroList[index];
            var weapon = equipment as WeaponBase;
            var armor = equipment as ArmorBase;
            if (weapon != null)
            {
                adventurer.Weapon = value ? weapon : new NoWeapon();
            }
            else if (armor != null)
            {
                adventurer.Armor = value ? armor : new NoArmor();
            }

            RecalcInvestmentAndStake(index);
            CheckBuyable();
        }

        protected override void FillAdventurerListItem(Unit adventurer, int index)
        {
            GameObject listItem;
            listItem = Instantiate(ListItemPrefab);
            listItem.transform.SetParent(gameObject.transform);
            listItem.transform.localScale = Vector3.one;
            listItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TavernUtil.getPortrait(adventurer);
            var stats = listItem.transform.GetChild(1);
            stats.GetChild(1).gameObject.GetComponent<Text>().text = adventurer.Name;
            stats.GetChild(3).gameObject.GetComponent<Text>().text = adventurer.Level.ToString();
            stats.GetChild(5).gameObject.GetComponent<Text>().text = adventurer.UnitClass.UnitType.ToString();

            UpdateIndex(index);
            RecalcInvestmentAndStake(index);
            CheckBuyable();
        }

        public override void UpdateIndex(int index)
        {
            var listItem = gameObject.transform.GetChild(index);
            Debug.Log("update: " + index);
            var button = listItem.GetChild(2).gameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();

            var toggleArmor = listItem.GetChild(3).gameObject.GetComponent<Toggle>();
            var toggleWeapon = listItem.GetChild(4).gameObject.GetComponent<Toggle>();
            button.onClick.AddListener(() =>
            {
                GameObject.Find("CoinSound").GetComponent<AudioSource>().Play();
                TavernControllerGo.GetComponent<TavernController>().Invest(index);
            });

            toggleArmor.onValueChanged.RemoveAllListeners();
            toggleArmor.onValueChanged.AddListener(value => ToggleEquipment(new ChainmailArmor(), value, index));
            toggleWeapon.onValueChanged.RemoveAllListeners();
            toggleWeapon.onValueChanged.AddListener(value => ToggleEquipment(new Sword(), value, index));
        }
    }
}