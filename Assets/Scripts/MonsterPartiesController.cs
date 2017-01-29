﻿using System.Collections.Generic;
using Model;
using Model.Util;
using UnityEngine;
using Util;

public class MonsterPartiesController : MonoBehaviour {

    public GameObject PartyContainerPrefab;

    private GameState _gameState;
    private WorldGraphController _graphController;
    private Dictionary<Party, PartyContainer> _partyContainers;
    private bool _initiatedMove;

    private void Start()
    {
        _gameState = GetComponentInParent<WorldLoopManager>().GameState;
        _graphController = GetComponentInParent<WorldGraphController>();
    }

    private void Update()
    {
        if (_partyContainers == null)
        {
            if (_gameState.Monsters != null)
            {
                _partyContainers = new Dictionary<Party, PartyContainer>();
                foreach (var party in _gameState.Monsters)
                {
                    var gObject = Instantiate(PartyContainerPrefab);
                    var partyContainer = gObject.GetComponent<PartyContainer>();

                    partyContainer.Party = party;
                    gObject.transform.parent = transform;
                    gObject.transform.position = _graphController.GetNodePositionOnMap(party.CurrentNode);

                    _partyContainers.Add(party, partyContainer);
                }
            }

            return;
        }

        if (_gameState.MonsterPartyMoving && !_initiatedMove)
        {
            var position = _graphController.GetNodePositionOnMap(_gameState.MovingMonsterParty.CurrentNode);

            var moveTo = _partyContainers[_gameState.MovingMonsterParty].gameObject.AddComponent<MoveTo>();
            moveTo.Move(position, GameData.MoveAnimationTime, () =>
            {
                _gameState.MonsterPartyMoving = false;
                _initiatedMove = false;
            });

            _initiatedMove = true;
        }
    }
    
}
