using UnityEngine;

public class BattleView : MonoBehaviour
{
    private HeroPartyController _partyController;
    private WorldLoopManager _loopController;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
	void Start ()
	{
	    _partyController = GetComponentInParent<HeroPartyController>();
	    _loopController = GetComponentInParent<WorldLoopManager>();
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _spriteRenderer.enabled = _partyController != null && _loopController.GameState.BatteRunning;
        GetComponent<AudioSource>().mute = !(_partyController != null && _loopController.GameState.BatteRunning);
        
	}
}
