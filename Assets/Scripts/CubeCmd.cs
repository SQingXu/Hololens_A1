using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCmd : MonoBehaviour {

    Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        originalPosition = this.transform.localPosition;
	}
	
    void OnSelect()
    {
        if (!this.GetComponent<Rigidbody>())
        {
            Debug.Log("Clicked");

        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
