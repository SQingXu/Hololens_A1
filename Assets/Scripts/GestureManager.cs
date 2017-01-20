using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {
    public static GestureManager Instance { get; private set; }
    public GameObject selectedObject;
    GestureRecognizer recognizer;
	// Use this for initialization
	void Start () {
        Instance = this;
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapTimes, ray) =>
        {
            if (selectedObject != null)
            {
                selectedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject beforeSelectedObject = selectedObject;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition,gazeDirection, out hitInfo))
        {
            selectedObject = hitInfo.collider.gameObject;
        }
        else
        {
            selectedObject = null;
        }
        if (beforeSelectedObject != selectedObject)
        {
            Debug.Log("Object Change");
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
	}
}
