using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {
    public static GestureManager instance;
    public static GestureManager Instance {
        get
        {
            if (instance == null)
            {
                instance = new GestureManager();
                
            }
            return instance;
        }
    }
    public GameObject selectedObject;
    GestureRecognizer drawRecognizer;
    GestureRecognizer manipRecognizer;
    public GameObject prefabSphere;
    public GameObject prefabCube;
    public GameObject prefabObject;
    GameObject[] createdObjects;
    Vector3 PreviousPosition;
    Vector3 MoveVector;
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
        drawRecognizer = new GestureRecognizer();
        drawRecognizer.TappedEvent += (source, tapTimes, ray) =>
        {
            audioSource.clip = clickClip;
            audioSource.Play();
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
            createdObjects[object_num] = Instantiate(prefabObject, position, transform.rotation);
            createdObjects[0].GetComponent<Renderer>().material.color = Color.green;

            object_num++;
            
        };
        drawRecognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void StopDrawing()
    {
        drawRecognizer.CancelGestures();
        drawRecognizer.StopCapturingGestures();
    }
    void StartDrawing()
    {
        drawRecognizer.StartCapturingGestures();
    }
    void StartMovingObjects()
    {
        audioSource.clip = shapeClip;
        audioSource.Play();
        Debug.Log("Start Moving");
        StopDrawing();
        manipRecognizer.StartCapturingGestures();
    }
    void StopMovingObjects()
    {
        audioSource.clip = shapeClip;
        audioSource.Play();
        Debug.Log("Stop Moving");
        StartDrawing();
        manipRecognizer.CancelGestures();
        manipRecognizer.StopCapturingGestures();
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
    private void Awake()
    {
        manipRecognizer = new GestureRecognizer();
        manipRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);
        manipRecognizer.ManipulationStartedEvent += manipRecognizer_ManipulationStartedEvent;
        manipRecognizer.ManipulationUpdatedEvent += manipRecognizer_ManipulationUpdatedEvent;
        //manipRecognizer.ManipulationCompletedEvent += manipRecognizer_ManipulationCompletedEvent;
        //manipRecognizer.ManipulationCanceledEvent += manipRecognizer_ManipulationCanceledEvent;

    }
    private void OnDestroy()
    {
        manipRecognizer.ManipulationStartedEvent -= manipRecognizer_ManipulationStartedEvent;
        manipRecognizer.ManipulationUpdatedEvent -= manipRecognizer_ManipulationUpdatedEvent;
        //manipRecognizer.ManipulationCompletedEvent -= manipRecognizer_ManipulationCompletedEvent;
        //manipRecognizer.ManipulationCanceledEvent -= manipRecognizer_ManipulationCanceledEvent;
    }
    private void manipRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        PreviousPosition = position;
        Debug.Log("Start");
        
    }
    private void manipRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
            Debug.Log(position);
            MoveVector = position - PreviousPosition;
            PreviousPosition = position;
            transformDrawing();

    }
    //private void manipRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    //{

    //}
    //private void manipRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    //{

    //}
    private void transformDrawing()
    {
        for (int i = 0; i < object_num; i++)
        {
            createdObjects[i].transform.position += MoveVector;
        } 
    } 
}
