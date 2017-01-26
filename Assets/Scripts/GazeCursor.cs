using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour {
    private MeshRenderer meshRenderer;
    //public GameObject cubeCursorPrefab;
    //public GameObject sphereCursorPrefab;
	// Use this for initialization
	void Start () {
        //sphereCursorPrefab.SetActive(true);
        //cubeCursorPrefab.SetActive(false);
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //var headPosition = Camera.main.transform.position;
        //var gazeDirection = Camera.main.transform.forward;
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
        meshRenderer.enabled = true;
        //RaycastHit hitInfo;

        //if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        //{
        //    meshRenderer.enabled = true;
        //    this.transform.position = hitInfo.point;
        //    this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        //}
        //else
        //{
        //    meshRenderer.enabled = false;
        //}
    }

}
