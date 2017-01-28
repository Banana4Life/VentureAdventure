using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tavern
{
    public class TavernLoader : MonoBehaviour
    {
        public void Awake()
        {
            SceneManager.LoadScene("Tavern", LoadSceneMode.Additive);
        }

        public void ShowTavern()
        {
            var rootGos = SceneManager.GetSceneByName("Tavern").GetRootGameObjects();
            rootGos[0].SetActive(true);
            rootGos[2].GetComponent<AudioSource>().mute = false;
        }
    }
}