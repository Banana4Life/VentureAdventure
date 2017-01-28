using UnityEngine;
using World;

namespace Model
{
    internal class ObjectiveController : MonoBehaviour
    {
        public NodeController NodeController { get; set; }
        public Objective Objective { get; set; }
    }
}