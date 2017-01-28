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

            if (validLinkSelection())
            {
                DrawDefaultInspector();
                var left = Selection.gameObjects.First();
                var right = Selection.gameObjects.Last();
                var graph = left.transform.parent.gameObject.GetComponent<WorldGraphController>();
                var leftCtrl = left.GetComponent<NodeController>();
                var rightCtrl = right.GetComponent<NodeController>();

                if (graph.IsConnected(leftCtrl, rightCtrl))
                {
                    if (GUILayout.Button("Unlink"))
                    {
                        graph.BreakLink(leftCtrl, rightCtrl);

                        Debug.Log("Unlinked " + left + " and " + right);
                    }
                }
                else
                {
                    if (GUILayout.Button("Link"))
                    {
                        graph.Link(leftCtrl, rightCtrl);

                        Debug.Log("Linked " + left + " and " + right);
                    }
                }
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