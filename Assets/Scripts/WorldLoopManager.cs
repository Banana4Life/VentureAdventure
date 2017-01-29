using System.Collections.Generic;
using UnityEngine;
using World;

namespace Model
{
    [RequireComponent(typeof(WorldGraphController))]
    public class WorldLoopManager : MonoBehaviour
    {
        private readonly GameState _state = new GameState();
        private WorldGraphController _graphController;
        private List<GameStep> _preparationSteps;
        private List<GameStep> _gameLoopSteps;
        private int _currentStep = -1;
        

        void Start()
        {
            _graphController = GetComponent<WorldGraphController>();
            _state.WorldGraph = _graphController.WorldGraph;

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
                new MoveMonsterPartiesStep(_state),
                new ExecuteFightStep(_state),
                new PickupLootStep(_state),
                new CheckForTargetStep(_state)
            };

            _state.PreparingRound = true;
        }

        void Update()
        {
            if (_state.PreparingRound)
            {
                if (_currentStep >= 0 && !_preparationSteps[_currentStep].Complete) return;

                _currentStep++;

                if (_currentStep > _preparationSteps.Count)
                {
                    _state.PreparingRound = false;
                    _currentStep = -1;
                    return;
                }

                StartCoroutine(_preparationSteps[_currentStep].DoLoop());
            }
            else
            {
                if (_currentStep >= 0 && !_gameLoopSteps[_currentStep].Complete) return;

                _currentStep++;

                if (_currentStep > _gameLoopSteps.Count)
                {
                    if (_state.RoundFinished)
                    {
                        _state.PreparingRound = true;
                        _state.RoundFinished = false;
                    }

                    _currentStep = -1;
                    return;
                }

                StartCoroutine(_gameLoopSteps[_currentStep].DoLoop());
            }
        }
    }
}