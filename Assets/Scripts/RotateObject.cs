using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 360);
	    var x = (Random.value - 0.5f)/5 + 1f;
	    var y = (Random.value - 0.5f)/5 + 1f;
        transform.localScale = new Vector3(x, y);
	}
}
