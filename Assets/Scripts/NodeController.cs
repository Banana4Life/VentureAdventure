using System;
using UnityEngine;

namespace World
{
    [ExecuteInEditMode]
    [Serializable]
    public class NodeController : MonoBehaviour
    {
        [SerializeField]
        public Node node = new Node();

        public Node Node
        {
            get { return node; }
        }
    }
}