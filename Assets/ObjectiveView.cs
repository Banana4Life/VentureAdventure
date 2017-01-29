using Model;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveView : MonoBehaviour
{
    public Objective Objective;
    private TextMesh _amountText;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _amountText = GetComponentInChildren<TextMesh>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
	void Update ()
    {
	    if (Objective != null)
	    {
	        _amountText.text = string.Format("{0}G", Objective.GoldReward);
	        _spriteRenderer.enabled = Objective.IsSelected;
	    }
	}
}
