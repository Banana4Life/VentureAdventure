using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using World;

namespace Model
{
    class PartyContainer : MonoBehaviour
    {
        public List<UnitVisualizer> members = new List<UnitVisualizer>();
        public NodeController NodeController { get; set; }
        public bool IsHiddenParty { get; set; }
        public Node Node { get; set; }
    }
}
