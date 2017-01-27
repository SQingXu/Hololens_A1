using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class HandsManager : MonoBehaviour {
    public bool HandDetected = false;
    public bool HandPrssed = false;
    public static HandsManager Instance = new HandsManager();
    private void Awake()
    {
        InteractionManager.SourceDetected += InteractionManager_SourceDetected;
        InteractionManager.SourceLost += InteractionManager_SourceLost;
        InteractionManager.SourcePressed += InteractionManager_SourcePressed;
        InteractionManager.SourceReleased += InteractionManager_SourceReleased;

    }
    // Use this for initialization
    void Start () {
		
	}
    private void InteractionManager_SourceDetected(InteractionSourceState hand)
    {
        HandDetected = true;
    }
    private void InteractionManager_SourceLost(InteractionSourceState hand)
    {
        HandDetected = false;
    }
    private void InteractionManager_SourcePressed(InteractionSourceState hand)
    {
        HandPrssed = true;
    }
    private void InteractionManager_SourceReleased(InteractionSourceState hand)
    {
        HandPrssed = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
