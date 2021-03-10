using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Camera parentCamera;
    public Camera childCamera;
    public Camera ghostCamera;
    public CameraAgent agent;

    void Start()
    {
        ghostCamera.enabled = false;
        childCamera.enabled = false;
        parentCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            parentCamera.enabled = false;
            ghostCamera.enabled = true;
            childCamera.enabled = false;
            agent.Shift();
        }

        if(!agent.isShifting && !(childCamera.enabled || parentCamera.enabled))
        {
            switch (agent.state)
            {
                default:
                case -1:
                    parentCamera.enabled = false;
                    ghostCamera.enabled = false;
                    childCamera.enabled = true;
                    break;
                case 1:
                    parentCamera.enabled = true;
                    ghostCamera.enabled = false;
                    childCamera.enabled = false;
                    break;
            }

        }
    }
}
