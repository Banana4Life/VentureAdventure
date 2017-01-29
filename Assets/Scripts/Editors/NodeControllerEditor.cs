using System.Linq;
using UnityEditor;
using UnityEngine;

namespace World
{
    [CustomEditor(typeof(NodeController))]
    [CanEditMultipleObjects]
    public class NodeControllerEditor : Editor
    {
        private bool validLinkSelection()
        {
            if (Selection.gameObjects.Length == 2)
            {
                foreach (var gameObject in Selection.gameObjects)
                {
                    if (gameObject.GetComponent<NodeController>() == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();


            if (validLinkSelection())
            {
                var left = Selection.gameObjects.First();
                var right = Selection.gameObjects.Last();
                var graphController = left.GetComponentInParent<WorldGraphController>();
                var leftCtrl = left.GetComponent<NodeController>();
                var rightCtrl = right.GetComponent<NodeController>();

                if (graphController.WorldGraph.IsConnected(leftCtrl.Node, rightCtrl.Node))
                {
                    if (GUILayout.Button("Unlink"))
                    {
                        graphController.WorldGraph.RemoveConnection(leftCtrl.Node, rightCtrl.Node);

                        Debug.Log("Unlinked " + left + " and " + right);
                    }
                }
                else
                {
                    if (GUILayout.Button("CreateConnection"))
                    {
                        graphController.WorldGraph.CreateConnection(leftCtrl.Node, rightCtrl.Node);

                        Debug.Log("Linked " + left + " and " + right);
                    }
                }

                graphController.Update();

                serializedObject.Update();
                serializedObject.ApplyModifiedProperties();
            }
        }
        private bool validStartSelection()
        {
            return Selection.gameObjects.Length == 1 && Selection.gameObjects.First().GetComponent<NodeController>() != null;
        }
    }
}