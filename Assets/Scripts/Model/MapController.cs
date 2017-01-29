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
        }

        public void ShowMap()
        {
            GameObject.Find("MapMusic").GetComponent<AudioSource>().mute = false;
        }

        public void HideMap()
        {
            GameObject.Find("MapMusic").GetComponent<AudioSource>().mute = true;
        }
    }
}