using Model;
using Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
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
        if (_gameState.Objectives == null)
        {
            foreach (var pair in _objectiveObjects)
            {
                Destroy(pair.Value);
            }

            _objectiveObjects.Clear();

            return;
        }

        foreach (var objective in _gameState.Objectives)
        {
            if (!_objectiveObjects.ContainsKey(objective) && !objective.IsClaimed)
            {
                var gameObj = Instantiate(ObjectivePrefab);

                var view = gameObj.GetComponent<ObjectiveView>();
                view.Objective = objective;

                gameObj.transform.SetParent(transform);
                gameObj.transform.position = _graphController.GetNodePositionOnMap(objective.Node);
                _objectiveObjects[objective] = gameObj;

                var button = gameObj.GetComponent<Button>();
                var clickedEvent = new Button.ButtonClickedEvent();
                button.onClick = clickedEvent;

                var obj = objective;
                clickedEvent.AddListener(() =>
                {
                    if (!_gameState.TargetSelected)
                    {
                        obj.IsSelected = true;
                        _gameState.SelectedTarget = obj.Node;
                        _gameState.TargetSelected = true;
                        button.enabled = false;
                    }
                });
            }

            if (_objectiveObjects.ContainsKey(objective) && objective.IsClaimed)
            {
                Destroy(_objectiveObjects[objective]);
                _objectiveObjects.Remove(objective);
            }
        }
    }
}