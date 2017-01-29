using System.Linq;
using Model;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class UnitVisualizer : MonoBehaviour
{
    public UnitAnimation[] Animations;

    public Unit Unit;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Unit == null) return;

        if (!Unit.IsAlive)
        {
            Destroy(gameObject);
        }

        var anim = Animations.First(sprite => sprite.UnitType == Unit.UnitClass.UnitType);
        _animator.runtimeAnimatorController = anim.Controller;
        _animator.speed = anim.speed;

    }
}
