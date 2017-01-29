using Model;
using Model.World;
using System;
using UnityEngine;


//[Serializable]
//public struct ObjectiveSprite
//{
//	public ObjectiveType ObjectiveType;
//	public Sprite Sprite;
//}


internal class ObjectiveController : MonoBehaviour
{
    private GameState _gameState;
    private SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _gameState = GetComponentInParent<WorldLoopManager>().GameState;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (_gameState.Objectives == null || _gameState.Objectives.Count == 0) return;

        var objectives = _gameState.Objectives;
        foreach (var objective in objectives)
        {
            var position = GetComponentInParent<WorldGraphController>().GetNodePositionOnMap(objective.Node);
            this.gameObject.transform.localPosition = position;
            //_spriteRenderer.sprite = Sprites.First(sprite => sprite.ObjectiveType == objective.ObjectiveType).Sprite; 
        }
    }



}