using Model;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveView : MonoBehaviour
{
    public Objective Objective;
    private TextMesh _amountText;

    void Start()
    {
        _amountText = GetComponentInChildren<TextMesh>();
    }
    
	void Update ()
    {
	    if (Objective != null)
	    {
	        _amountText.text = string.Format("{0}G", Objective.GoldReward);
	    }
	}
}
