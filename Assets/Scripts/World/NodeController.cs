using System;
using System.Linq;
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

        //void Update()
        //{

        //    if (Application.isPlaying) return;

        //    var nodeControllers = transform.parent.GetComponentsInChildren<NodeController>();

        //    foreach (var controller in nodeControllers)
        //    {
        //        if (controller.Node.Id == -1)
        //        {
        //            controller.Node.Id = nodeControllers.Max(n => n.Node.Id) + 1;
        //            controller.name = "Node Controller " + controller.Node.Id;
        //        }
        //    }
        //}
    }
}