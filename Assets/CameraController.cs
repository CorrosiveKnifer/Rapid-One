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
    public Camera parentCamera;
    public Camera childCamera;
    public Camera ghostCamera;
    public CameraAgent agent;

    void Start()
    {
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
            parentCamera.enabled = false;
            ghostCamera.enabled = true;
            childCamera.enabled = false;

            agent.Shift();
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
                    break;
                case 1: //Parent Transform
                    parentCamera.enabled = true;
                    childCamera.enabled = false;
                    break;
            }
            ghostCamera.enabled = false;
        }
    }
}
