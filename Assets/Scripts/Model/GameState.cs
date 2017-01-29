using System.Collections.Generic;
using World;

namespace Model
{
    public class GameState
    {
        public WorldGraph WorldGraph { get; set; }
        public List<Objective> Objectives { get; set; }
        public List<Party> Monsters { get; set; }
        public Node SelectedTarget { get; set; }
        public Party HeroParty { get; set; }
        public bool HeroPartyMoving { get; set; }
        public bool MonsterPartiesMoving { get; set; }
        public bool BatteRunning { get; set; }
        public bool RoundFinished { get; set; }
        public int PlayedRounds { get; set; }
        public bool PreparingRound { get; set; }
        public int Money { get; set; }
    }
}