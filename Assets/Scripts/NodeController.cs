using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodeController : MonoBehaviour
{
    [SerializeField]
    public Node node;

    [SerializeField]
    public List<NodeController> neighbors = new List<NodeController>();


}