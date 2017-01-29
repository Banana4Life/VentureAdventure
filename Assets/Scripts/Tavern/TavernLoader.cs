using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tavern
{
    public class TavernLoader : MonoBehaviour
    {
        public GameObject RoundsText;

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

        public void Update()
        {
            var rootGos = SceneManager.GetSceneByName("Map").GetRootGameObjects();
            var worldGraph = rootGos[0];
            foreach (var go in rootGos)
            {
                if (go.name != "GameRoot")
                    continue;
                worldGraph = go;
                break;
            }
            var worldLoopManager = worldGraph.GetComponent<WorldLoopManager>();
            RoundsText.GetComponent<TextMesh>().text = "Round: " + worldLoopManager.GameState.PlayedRounds;
        }
    }
}