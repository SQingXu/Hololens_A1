using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    // Use this for initialization
    public bool rotation = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rotation == true)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * 10);
        }
        
	}
}
