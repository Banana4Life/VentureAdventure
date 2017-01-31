using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScence : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked()
    {
        SceneManager.LoadScene("Map");
    }
}
