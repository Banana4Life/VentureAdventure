using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model
{
   public class MapController : MonoBehaviour
    {
        public void Awake()
        {
          //  SceneManager.LoadScene("Map", LoadSceneMode.Additive);
        }

        public void ShowMap()
        {
            //var rootGos = SceneManager.GetSceneByName("Map").GetRootGameObjects();
            //foreach (var go in rootGos)
            //{
            //  //  if (go.name == "GameRoot")
            //  //      go.SetActive(true);
            //    //else 
            //    if (go.name == "MapMusic")
            //    {
            //        go.GetComponent<AudioSource>().mute = false;
            //    }
            //}
            GameObject.Find("MapMusic").GetComponent<AudioSource>().mute = false;
        }

        public void HideMap()
        {
            //GameObject.Find("GameRoot").SetActive(false);
            GameObject.Find("MapMusic").GetComponent<AudioSource>().mute = true;
        }
    }
}