using System.Linq;
using Model;
using UnityEngine;
using Util;

namespace World
{
    public class TestNodeMover : MonoBehaviour
    {
        public WorldGraphController graph;
        private NodeController previousNode;
        public NodeController currentNode;
        private NodeController nextNode;

        private void Update()
        {
            if (nextNode == null)
            {
                var possibleNext = graph.GetNeighborsOf(currentNode).Where(n => n != previousNode).ToList();
                if (possibleNext.Count == 0)
                {
                    nextNode = previousNode;
                }
                else
                {
                    nextNode = possibleNext.Random();
                }
                gameObject.AddComponent<MoveTo>().Move(nextNode.gameObject.transform.position, 0.3f);
            }
        }

        void OnMovementCompleted()
        {
            previousNode = currentNode;
            currentNode = nextNode;
            nextNode = null;
        }
    }
}