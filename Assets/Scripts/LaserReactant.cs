using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReactant : MonoBehaviour
{
    public UnityEvent m_OnPower;
    public UnityEvent m_OffPower;

    public bool IsActivated = false;
    public bool IsPowered = false;

    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool currentpower = IsPowered;
        IsPowered = IsActivated;
        if (currentpower != IsPowered)
        {
            if (IsPowered)
            {
                m_OnPower.Invoke();
                if (GameManager.instance.GameTime > 1.0f && PlayerController.instance.m_isAdultForm)
                {
                    GetComponent<AudioAgent>().PlaySoundEffect("LaserOpen");
                }
                else if (GameManager.instance.GameTime > 1.0f)
                {
                    GetComponent<AudioAgent>().PlaySoundEffect("FlameOn");
                }
            }
            else
            {
                m_OffPower.Invoke();
                if (GameManager.instance.GameTime > 1.0f && PlayerController.instance.m_isAdultForm)
                {
                    GetComponent<AudioAgent>().PlaySoundEffect("LaserClose");
                }
                else if (GameManager.instance.GameTime > 1.0f)
                {
                    GetComponent<AudioAgent>().PlaySoundEffect("FlameOff");
                }
            }
        }
        if (IsActivated)
        {
            IsActivated = false;
        }

    }
    public void OpenDoor()
    {
        door.GetComponentInChildren<DoorScript>().Unlock(false);
        door.GetComponentInChildren<DoorScript>().OpenDoor(false);
    }
    public void CloseDoor()
    {
        door.GetComponentInChildren<DoorScript>().Lock();
        door.GetComponentInChildren<DoorScript>().CloseDoor(false); 
    }
    public void Activated()
    {
        IsActivated = true;
    }
 
}
