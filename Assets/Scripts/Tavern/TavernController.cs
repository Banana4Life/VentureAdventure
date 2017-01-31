using UnityEngine;
using UnityEngine.UI;

namespace Tavern
{
    public class TavernController : MonoBehaviour
    {
        public GameObject MoneyText;
        public GameObject InvestmentPanel;
        public GameObject DeadText;

        public GameObject InvestableHeroListGo;
        public GameObject InvestedHeroListGo;
        public GameObject PartyGo;

        private InvestableHeroList investableHeroList;
        private InvestedHeroList investedHeroList;
        private Party party;

        public void OnEnable()
        {
            investableHeroList = InvestableHeroListGo.GetComponent<InvestableHeroList>();
            investedHeroList = InvestedHeroListGo.GetComponent<InvestedHeroList>();
            party = PartyGo.GetComponent<Party>();
            UpdateMoney();
        }

        public void OpenInvestmentPanel()
        {
            var count = investableHeroList.Count;
            investableHeroList.CheckBuyable();
            for (var i = count; i < 5 - investedHeroList.Count; i++)
            {
                var adventurer = TavernUtil.generateNewRandomAdventurer();
                investableHeroList.Add(adventurer);
            }
            InvestmentPanel.SetActive(true);
        }

        public void CloseInvestmentPanel()
        {
            InvestmentPanel.SetActive(false);
        }

        public void Invest(int index)
        {
            Debug.Log("invest: " + index);
            var adventurer = investableHeroList.Get(index);
            if (TavernUtil.getAdventurerWorth(adventurer) <= TavernUtil.getGameState().Money)
            {
                TavernUtil.getGameState().Money -= TavernUtil.getAdventurerWorth(adventurer);
                var investedIndex = investedHeroList.Add(adventurer);
                if (party.adventuring)
                {
                    investedHeroList.DeactivateAdventurer(investedIndex);
                }
                investableHeroList.RemoveAt(index);
                UpdateMoney();
                investableHeroList.CheckBuyable();
                adventurer.Stake = TavernUtil.getStake(adventurer);
            }
            else
            {
                Debug.LogWarning("Tried to invest without enough money");
            }
        }

        public void AddToParty(int index)
        {
            var adventurer = investedHeroList.Get(index);
            party.Add(adventurer, index);
            investedHeroList.DeactivateAdventurer(index);
        }

        public void HideTavern()
        {
            GameObject.Find("TavernCanvas").SetActive(false);
            GameObject.Find("TavernMusic").GetComponent<AudioSource>().mute = true;
        }

        private bool wereGone = false;
        private int deadPeople = 0;

        private void Update()
        {
            var partyCount = party.Count;
            for (var i = partyCount - 1; i >= 0; i--)
            {
                if (!party.Get(i).IsAlive)
                {
                    deadPeople++;
                    DeadText.GetComponent<Text>().text = deadPeople.ToString();
                    KillAdventurer(i);
                }
            }
            if (TavernUtil.getGameState().HeroParty != null && TavernUtil.getGameState().HeroParty.CurrentNode != TavernUtil.getGameState().WorldGraph.TavernNode)
            {
                wereGone = true;
            }
            if (TavernUtil.getGameState().PreparingRound && party.adventuring && wereGone)
            {
                wereGone = false;
                PartyHome();
            }
        }



        public void SendParty()
        {
            TavernUtil.getGameState().HeroParty = party.GetParty();
            HideTavern();
            PartyAdventuring();
        }

        private void PartyAdventuring()
        {
            party.StartAdventuring();
            var count = investedHeroList.Count;
            for (var i = 0; i < count; i++)
            {
                investedHeroList.DeactivateAdventurer(i);
            }
        }

        public void PartyHome()
        {
            party.ReturnHome();
            var count = investedHeroList.Count;
            for (var i = 0; i < count; i++)
            {
                investedHeroList.ActivateAdventurer(i);
            }
        }

        public void UpdateMoney()
        {
            MoneyText.GetComponent<Text>().text = TavernUtil.getGameState().Money + " $";
        }

        private void KillAdventurer(int partySlot)
        {
            var index = party.GetIndex(partySlot);
            investedHeroList.RemoveAt(index);
            party.RemoveAt(partySlot);
            for (var i = 0; i < party.Count; i++)
            {
                if (party.GetIndex(i) > index)
                {
                    party.SetIndex(i, party.GetIndex(i) - 1);
                }
            }
        }

        public void RemoveFromParty(int partySlot)
        {
            var index = party.GetIndex(partySlot);
            party.RemoveAt(partySlot);
            investedHeroList.ActivateAdventurer(index);
        }
    }
}