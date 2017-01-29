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
            foreach (var go in rootGos)
            {
                if (go.name == "TavernCanvas")
                {
                    go.SetActive(true);
                }
                else if (go.name == "TavernMusic")
                {
                    go.GetComponent<AudioSource>().mute = false;
                }
                else if (go.name == "TavernController")
                {
                    go.GetComponent<TavernController>().UpdateMoney();
                    go.GetComponent<TavernController>().CloseInvestmentPanel();
                }
            }
        }
    }
}