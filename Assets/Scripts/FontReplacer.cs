using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FontReplacer : MonoBehaviour
{

    public Font font;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (font == null) return;

	    foreach (var componentsInChild in GetComponentsInChildren<Text>())
	    {
	        Debug.Log(componentsInChild.name);
	        componentsInChild.font = font;
	        componentsInChild.fontSize = 30;
	    }
	}
}
