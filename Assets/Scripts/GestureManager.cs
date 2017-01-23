using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {
    public static GestureManager Instance { get; private set; }
    public GameObject selectedObject;
    GestureRecognizer recognizer;
    public GameObject prefabSphere;
	// Use this for initialization
	void Start () {
        Instance = this;
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapTimes, ray) =>
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
            GameObject prefabInstance = Instantiate(prefabSphere, position, transform.rotation);
            prefabInstance.SetActive(true);
            Debug.Log(Camera.main.nearClipPlane);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position, 1F);

            Debug.Log(position);
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
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
	}
}
