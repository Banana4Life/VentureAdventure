using System.Collections.Generic;
using UnityEngine;

namespace World
{
    [ExecuteInEditMode]
    public class NodeController : MonoBehaviour
    {
        [SerializeField]
        public Node node;

        [SerializeField]
        public List<NodeController> neighbors = new List<NodeController>();
    }
}