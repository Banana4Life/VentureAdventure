using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSplashToMap : MonoBehaviour {

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Map");
        }
    }
}
