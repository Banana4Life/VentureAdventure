using Model.World;

namespace Model
{
    public class Objective
    {
        public ObjectiveType ObjectiveType { get; set; }
        public int GoldReward { get; set; }
        public int Experience { get; set; }
        public Node Node { get; set; }
        public bool IsClaimed { get; set; }
        public bool IsSelected { get; set; }
    }
}