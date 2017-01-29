using System.Linq;
using Model;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UnitVisualizer : MonoBehaviour
{
    public UnitSprite[] Sprites;

    public Unit Unit;
    private SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (Unit == null) return;

        if (!Unit.IsAlive)
        {
            Destroy(gameObject);
        }

        _spriteRenderer.sprite = Sprites.First(sprite => sprite.UnitType == Unit.UnitClass.UnitType).Sprite; 
    }
}
