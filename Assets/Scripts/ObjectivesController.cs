using Model;
using Model.World;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


internal class ObjectivesController : MonoBehaviour
{
    private readonly Dictionary<Objective, GameObject> _objectiveObjects = new Dictionary<Objective, GameObject>();
    private GameState _gameState;
    public GameObject ObjectivePrefab;
    private WorldGraphController _graphController;

    public void Start()
    {
        _gameState = GetComponentInParent<WorldLoopManager>().GameState;
        _graphController = GetComponentInParent<WorldGraphController>();
    }

    public void Update()
    {
        if (_gameState.Objectives == null || _gameState.Objectives.Count == 0) return;

        foreach (var objective in _gameState.Objectives)
        {
            if (!_objectiveObjects.ContainsKey(objective) && !objective.IsClaimed)
            {
                var gameObj = Instantiate(ObjectivePrefab);
                gameObj.transform.parent = transform;
                gameObj.transform.position = _graphController.GetNodePositionOnMap(objective.Node);
                _objectiveObjects[objective] = gameObj;

                var button = gameObj.GetComponent<Button>();
                var clickedEvent = new Button.ButtonClickedEvent();
                button.onClick = clickedEvent;

                var obj = objective;
                clickedEvent.AddListener(() =>
                {
                    Debug.Log("Executing");
                    if (!_gameState.TargetSelected)
                    {
                        _gameState.SelectedTarget = obj.Node;
                        _gameState.TargetSelected = true;
                    }
                });
            }

            if (_objectiveObjects.ContainsKey(objective) && objective.IsClaimed)
            {
                _objectiveObjects.Remove(objective);
                Destroy(_objectiveObjects[objective]);
            }
        }
    }
}