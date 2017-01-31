using System.Linq;
using Model;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class UnitVisualizer : MonoBehaviour
{
    public UnitAnimation[] Animations;
    public Gradient HitPointBarGradient;

    public Unit Unit;
    private Animator _animator;
    private LineRenderer _hpBarRenderer;
    private SpriteRenderer _spriteRenderer;
    public bool Hidden { get; set; }

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _hpBarRenderer = GetComponentInChildren<LineRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (Unit == null) return;

        if (!Unit.IsAlive)
        {
            Destroy(gameObject);
        }

        _spriteRenderer.enabled = !Hidden;
        
        var anim = Animations.First(sprite => sprite.UnitType == Unit.UnitClass.UnitType);
        _animator.runtimeAnimatorController = anim.Controller;
        _animator.speed = anim.speed;
        
        _hpBarRenderer.enabled = Unit.CurrentHitPoints != Unit.MaxHitPoints;
        _hpBarRenderer.colorGradient = HitPointBarGradient;
        _hpBarRenderer.SetPosition(1, new Vector3(Mathf.Lerp(-0.17f,0.17f, (float) Unit.CurrentHitPoints/Unit.MaxHitPoints), -0.25f));

    }
}
