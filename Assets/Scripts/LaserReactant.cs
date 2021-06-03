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
                GetComponent<AudioAgent>().PlaySoundEffect("LaserOpen");
            }
            else
            {
                m_OffPower.Invoke();
                GetComponent<AudioAgent>().PlaySoundEffect("LaserClose");
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
