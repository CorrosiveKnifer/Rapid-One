using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class DoorScript : Interactable
{
    public uint KeyID = 0;
    public bool IsLocked = false;
    public bool CanOpenFromFront = true;
    public bool CanOpenFromBehind = true;
    public bool StartOpen = false;

    public enum OpenDirect { BOTH, FORWARD, BACKWARD};
    public OpenDirect myDirect;
    private bool isClosed = true;
    
    private Animator anim;
    private AudioAgent audio;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audio = GetComponentInChildren<AudioAgent>();

        isClosed = true;
        if(StartOpen)
        {
            Unlock();
            OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        IsLocked = false;
        audio.PlaySoundEffect("DoorUnlock");
    }
    public void Lock()
    {
        IsLocked = true;
    }

    public void OpenDoor(bool isOpeningForward = true)
    {
        if(anim.transform.localRotation.eulerAngles.y <= -85 || anim.transform.localRotation.eulerAngles.y >= 265)
        {
            //Checks if it is locked or blocked from one side.
            if (IsLocked || isOpeningForward && !CanOpenFromFront || !isOpeningForward && !CanOpenFromBehind)
            {
                anim.SetTrigger("OpenLocked");
                audio.PlaySoundEffect("DoorLocked");
                return;
            }

            //Opens door based on given direction
            switch (myDirect)
            {
                default:
                case OpenDirect.BOTH:

                    if (isOpeningForward)
                        anim.SetTrigger("OpenForward");
                    else if (!isOpeningForward)
                        anim.SetTrigger("OpenBackward");
                    break;
                case OpenDirect.FORWARD:
                    anim.SetTrigger("OpenBackward");
                    break;
                case OpenDirect.BACKWARD:
                    anim.SetTrigger("OpenForward");
                    break;
            }
            audio.PlaySoundEffectDelayed("DoorOpen", 0.05f);
            isClosed = false;
        }
    }

    public void CloseDoor()
    {
        if (anim.transform.localRotation.eulerAngles.y <= 5 || anim.transform.localRotation.eulerAngles.y >= 175)
        {
            anim.SetTrigger("Close");
            audio.PlaySoundEffectDelayed("DoorClosed", 0.85f);
            isClosed = true;
        } 
    }

    public override void Activate(Interactor other)
    {
        var dot = 1.0f;
        if(other != null)
        {
            //Calculate Orientation
            dot = Vector3.Dot(other.transform.position - transform.position, transform.right);
        }

        if (isClosed)
        {
            OpenDoor(dot > 0);
        }
        else
        {
            CloseDoor();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<KeyScript>() != null)
        {
            if(other.gameObject.GetComponent<KeyScript>().keyID == KeyID)
            {
                Destroy(other.gameObject);
                Unlock();
            }
        }
    }
}
