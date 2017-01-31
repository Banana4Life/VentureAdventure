using UnityEngine;
using UnityEngine.SceneManagement;

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
                switch (go.name)
                {
                    case "TavernCanvas":
                        go.SetActive(true);
                        break;
                    case "TavernMusic":
                        go.GetComponent<AudioSource>().mute = false;
                        break;
                    case "TavernController":
                        go.GetComponent<TavernController>().UpdateMoney();
                        go.GetComponent<TavernController>().CloseInvestmentPanel();
                        break;
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