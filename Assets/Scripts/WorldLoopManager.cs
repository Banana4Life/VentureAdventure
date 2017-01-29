using System.Collections.Generic;
using Model;
using Model.GameSteps;
using UnityEngine;

[RequireComponent(typeof(WorldGraphController))]
public class WorldLoopManager : MonoBehaviour
{
    public GameState GameState { get { return _state; } }

    private readonly GameState _state = new GameState();
    private WorldGraphController _graphController;
    private List<GameStep> _preparationSteps;
    private List<GameStep> _gameLoopSteps;
    private int _currentStep = -1;
        

    void Start()
    {
        _graphController = GetComponent<WorldGraphController>();
        _state.WorldGraph = _graphController.WorldGraph;
        _state.Money = 250;

        _preparationSteps = new List<GameStep>
        {
            new GenerateObjectivesStep(_state),
            new GenerateMonstersStep(_state),
            new WaitForObjectiveSelectionStep(_state),
            new WaitForPartySelectionStep(_state)
        };

        _gameLoopSteps = new List<GameStep>
        {
            new MovePlayerPartyStep(_state),
            new HealPlayersOnTavernStep(_state),
            new CheckForTargetStep(_state),
            new ExecuteFightStep(_state),
            new MoveMonsterPartiesStep(_state),
            new ExecuteFightStep(_state),
            new PickupLootStep(_state),
            new CheckForTargetStep(_state),
        };

        _state.PreparingRound = true;
    }

    void Update()
    {
        if (_state.PreparingRound)
        {
            if (_currentStep >= 0 && !_preparationSteps[_currentStep].Complete) return;

            _currentStep++;

            if (_currentStep >= _preparationSteps.Count)
            {
                _state.PreparingRound = false;
                _currentStep = -1;
                return;
            }

            Debug.Log("Now entering step: " + _preparationSteps[_currentStep].GetType().Name);

            StartCoroutine(_preparationSteps[_currentStep].StartWork());
        }
        else
        {
            if (_currentStep >= 0 && !_gameLoopSteps[_currentStep].Complete) return;

            _currentStep++;
            
            if (_currentStep >= _gameLoopSteps.Count || _state.RoundFinished)
            {
                if (_state.RoundFinished)
                {
                    ResetAfterRound();
                }

                _currentStep = -1;
                return;
            }
            

            Debug.Log("Now entering step: " + _gameLoopSteps[_currentStep].GetType().Name);

            StartCoroutine(_gameLoopSteps[_currentStep].StartWork());
        }
    }

    private void ResetAfterRound()
    {
        _state.PreparingRound = true;
        _state.RoundFinished = false;
        _state.PlayedRounds++;
        _state.Objectives = null;
        _state.Monsters = null;
        _state.HeroParty = null;
        _state.SelectedTarget = _state.WorldGraph.TavernNode;
        _state.TargetSelected = false;
    }
}