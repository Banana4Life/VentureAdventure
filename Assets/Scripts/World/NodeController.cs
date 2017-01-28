using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace World
{
    [ExecuteInEditMode]
    [Serializable]
    public class NodeController : MonoBehaviour
    {
        [SerializeField]
        public int Id = -1;

        void Update()
        {
            if (Application.isPlaying) return;

            var nodes = GetComponentsInChildren<NodeController>();

            foreach (var node in nodes)
            {
                if (node.Id == -1)
                {
                    node.Id = nodes.Max(n => n.Id) + 1;
                    node.name = "Node " + node.Id;
                }
            }
        }
    }
}