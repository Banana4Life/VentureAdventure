using Model;
using Model.Equipment.Armors;
using Model.Equipment.Weapons;
using Model.UnitClasses;
using Model.Util;
using UnityEngine;
using Util;

public class HeroPartyController : MonoBehaviour
{
    public GameObject PartyContainerPrefab;

    private GameState _gameState;
    private WorldGraphController _graphController;
    private PartyContainer _partyContainer;
    private bool _initiatedMove;

    private void Start()
    {
        _gameState = GetComponentInParent<WorldLoopManager>().GameState;
        _graphController = GetComponentInParent<WorldGraphController>();

        _gameState.HeroParty = new Party
        {
            CurrentNode = _graphController.TavernNodeController.Node,
        };

        _gameState.HeroParty.AddMember(new Unit
        {
            UnitClass = new FighterClass(),
            Armor = new ShirtArmor(),
            Weapon = new StickWeapon(),
            Name = "Harald Schmidt",
            Male = true
        });
        
    }

    private void Update()
    {
        if (_partyContainer == null)
        {
            if (_gameState.HeroParty != null)
            {
                var gObject = Instantiate(PartyContainerPrefab);
                _partyContainer = gObject.GetComponent<PartyContainer>();
                _partyContainer.Party = _gameState.HeroParty;
                gObject.transform.parent = transform;
                gObject.transform.position = PartyPosition;
            }

            return;
        }

        if (_gameState.HeroPartyMoving && !_initiatedMove)
        {
            var moveTo = _partyContainer.gameObject.AddComponent<MoveTo>();
            moveTo.Move(PartyPosition, GameData.MoveAnimationTime, () =>
            {
                _gameState.HeroPartyMoving = false;
                _initiatedMove = false;
            });

            _initiatedMove = true;
        }
    }

    private Vector3 PartyPosition
    {
        get { return _graphController.GetNodePositionOnMap(_gameState.HeroParty.CurrentNode); }
    }
}