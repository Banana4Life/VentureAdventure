using UnityEditor;

[CustomEditor(typeof(NodeController))]
public class ConnectoinDrawer : Editor
{
    void OnSceneGUI()
    {
        var node = target as NodeController;
        if (node != null)
        {
            var from = node.transform.position;
            foreach (var neighbor in node.neighbors)
            {
                Handles.DrawLine(from, neighbor.transform.position);
            }

        }
    }
}