using System;
using Model.World;
using UnityEngine;

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