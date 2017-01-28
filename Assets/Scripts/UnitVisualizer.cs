using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using UnityEngine;

[Serializable]
public struct UnitSprite
{
    public UnitType UnitType;
    public Sprite Sprite;
}

[RequireComponent(typeof(SpriteRenderer))]
public class UnitVisualizer : MonoBehaviour
{
    public UnitSprite[] Sprites;

    public Unit Unit;
    private SpriteRenderer _spriteRenderer;

    public void Update()
    {
        if (Unit == null) return;

        if (!Unit.IsAlive)
        {
            Destroy(gameObject);
        }

        _spriteRenderer.sprite = Sprites.First(sprite => sprite.UnitType == Unit.UnitClass.UnitType).Sprite; 
    }

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
