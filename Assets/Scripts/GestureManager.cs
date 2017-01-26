using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {
    public static GestureManager Instance { get; private set; }
    public GameObject selectedObject;
    GestureRecognizer recognizer;
    public GameObject prefabSphere;
    public GameObject prefabCube;
    public GameObject prefabObject;
    GameObject[] createdObjects;
    int object_num = 0;
    AudioSource audioSource = null;
    AudioClip clickClip = null;
    AudioClip shapeClip = null;

    // Use this for initialization
    void Start () {
        createdObjects = new GameObject[500];
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;
        prefabObject = prefabSphere;
        clickClip = Resources.Load<AudioClip>("Click");
        shapeClip = Resources.Load<AudioClip>("ChangeShape");
        Instance = this;
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapTimes, ray) =>
        {
                audioSource.clip = clickClip;
                //audioSource.PlayOneShot(clickClip,0.6f);
                audioSource.Play();
                Debug.Log("Play Sound");
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
                createdObjects[object_num] = Instantiate(prefabObject, position, transform.rotation);
                object_num++;
            
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
    void Cube()
    {
        audioSource.clip = shapeClip;
        audioSource.Play();
        prefabObject = prefabCube;
        Debug.Log("Now drawing cube");
    }
    void Sphere()
    {
        audioSource.clip = shapeClip;
        audioSource.Play();
        prefabObject = prefabSphere;
        Debug.Log("Now drawing sphere");
    }
    void Undo()
    {
        Debug.Log("Undo triggered");
        Destroy(createdObjects[object_num - 1]);
        object_num--;
    }
    void Clear()
    {
        for (int i = 0; i < object_num; i++)
        {
            Destroy(createdObjects[i]);
        }
        object_num = 0;
    }
}
