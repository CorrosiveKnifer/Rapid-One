using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <Author> Michael Jordan </Author>
/// </summary>
public class CameraController : MonoBehaviour
{
    #region SingletonImplementation
    public static CameraController instance { get; private set; }
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    //Cameras to transition between

    public GameObject parent;
    public GameObject child;
    public GameObject ghost;
    public GameObject ghostPostProcessing;

    private Camera parentCamera;
    private Camera childCamera;
    private Camera ghostCamera;

    public CameraAgent agent;
    public float transitionDelay = 0.3f;
    private float delay = 0.0f;

    void Start()
    {
        parentCamera = parent.GetComponentInChildren<Camera>();
        childCamera = child.GetComponentInChildren<Camera>();
        ghostCamera = ghost.GetComponentInChildren<Camera>();

        //Start initially at the parent camera.
        ghostCamera.enabled = false;
        childCamera.enabled = false;
        parentCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay = Mathf.Clamp(delay - Time.deltaTime, 0, transitionDelay); 
        }

        //If the left shift key is pressed start transitioning between using an agent.
        if (Input.GetKeyDown(KeyCode.LeftShift) && delay == 0 && !IsCameraShifting())
        {
            delay = transitionDelay;

            if (agent.currentState == CameraAgent.AgentState.FOLLOW_ADULT)
            {
                agent.currentState = CameraAgent.AgentState.FOLLOW_CHILD;
            }
            else
            {
                //Transition state: Child to Ghost
                parentCamera.enabled = false;
                ghostCamera.enabled = true;
                CopyRotationToGhost(child, childCamera);
                childCamera.enabled = false;

                agent.Shift();
            }
        }
        ghostPostProcessing.SetActive(IsCameraShifting());

        //If the agent has stopped shifting and the ghost camera is enabled.
        if (!IsCameraShifting() && ghostCamera.enabled)
        {
            //Ask the agent which transform it is at
            switch (agent.currentState)
            {
                case CameraAgent.AgentState.FOLLOW_ADULT:
                    parentCamera.enabled = true;
                    childCamera.enabled = false;
                    break;
                case CameraAgent.AgentState.FOLLOW_CHILD:
                    parentCamera.enabled = false;
                    childCamera.enabled = true;
                    break;

                default:
                case CameraAgent.AgentState.SHIFTTING:
                    Debug.LogError("Impossible case reached(Agent shifting and not shifting at the same time)?");
                    break;
            }
            ghostCamera.enabled = false;
        }
    }

    public bool IsCameraShifting()
    {
        return agent.currentState == CameraAgent.AgentState.SHIFTTING;
    }

    void CopyRotationToGhost(GameObject other, Camera otherCam)
    {
        ghost.transform.rotation = other.transform.rotation;
        ghostCamera.transform.rotation = otherCam.transform.rotation;
    }
}
