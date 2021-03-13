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
            instance = new CameraController();
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

    private Camera parentCamera;
    private Camera childCamera;
    private Camera ghostCamera;

    public CameraAgent agent;

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
        //If the left shift key is pressed start transitioning between using an agent.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(agent.state == 1)
            {
                agent.state = -1;
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

        //If the agent has stopped shifting and the ghost camera is enabled.
        if(!agent.isShifting && ghostCamera.enabled)
        {
            //Ask the agent which transform it is at
            switch (agent.state)
            {
                default:
                case -1: //Child Transform
                    parentCamera.enabled = false;
                    childCamera.enabled = true;
                    CopyRotationToChild(parent, parentCamera);
                    break;
                case 1: //Parent Transform
                    parentCamera.enabled = true;
                    childCamera.enabled = false;
                    CopyRotationToParent(ghost, ghostCamera);
                    break;
            }
            ghostCamera.enabled = false;
        }
    }

    void CopyRotationToParent(GameObject other, Camera otherCam)
    {
        parent.transform.rotation = other.transform.rotation;
        parentCamera.transform.rotation = otherCam.transform.rotation;
    }

    void CopyRotationToChild(GameObject other, Camera otherCam)
    {
        child.transform.rotation = other.transform.rotation;
        childCamera.transform.rotation = otherCam.transform.rotation;
    }

    void CopyRotationToGhost(GameObject other, Camera otherCam)
    {
        ghost.transform.rotation = other.transform.rotation;
        ghostCamera.transform.rotation = otherCam.transform.rotation;
    }
}
