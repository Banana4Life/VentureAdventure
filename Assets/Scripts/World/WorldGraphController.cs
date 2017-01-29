using Model;
using Model.UnitClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace World
{
    [ExecuteInEditMode]
    public class WorldGraphController : MonoBehaviour
    {
        public GameObject ConnectionPrefab;
        public NodeController TavernNodeController;

        [SerializeField]
        public WorldGraph WorldGraph = new WorldGraph();

        private Dictionary<Node, NodeController> _nodeControllers; 

        // Use this for initialization
        void Start ()
        {
            if (!Application.isPlaying) return;

            _nodeControllers = GetComponentsInChildren<NodeController>().ToDictionary(nc => nc.Node);


            List<Unit> units = new List<Unit>();
            units.Add(new Unit {UnitClass = new FighterClass()});
            units.Add(new Unit {UnitClass = new PriestClass()});
            units.Add(new Unit {UnitClass = new RangerClass()});

            GetComponent<HeroController>().SpawnHeros(units, this);

            foreach (var connection in WorldGraph.Connections)
            {
                var startController = _nodeControllers[connection.Start];
                var endController = _nodeControllers[connection.End];


                CreateLine(startController, endController);
            }
        }

        public void Update()
        {
            if (Application.isPlaying) return;

            _nodeControllers = GetComponentsInChildren<NodeController>().ToDictionary(nc => nc.Node);

            foreach (var connection in WorldGraph.Connections)
            {
                var startController = _nodeControllers[connection.Start];
                var endController = _nodeControllers[connection.End];
                Debug.DrawLine(startController.transform.position, endController.transform.position);
            }
        }

        private GameObject CreateLine(NodeController left, NodeController right)
        {
            var connectionLine = Instantiate(ConnectionPrefab);
            connectionLine.transform.parent = gameObject.transform;
            var lineRenderer = connectionLine.GetComponent<LineRenderer>();
            lineRenderer.SetPositions(new []{left.gameObject.transform.position, right.gameObject.transform.position});
            return connectionLine;
        }

    }
}