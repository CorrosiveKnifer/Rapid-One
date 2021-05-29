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
    public bool IsInteractable = true;

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
            Unlock(false);
            OpenDoor(true, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock(bool hasAudio = true)
    {
        IsLocked = false;

        if(hasAudio)
        {
            audio.PlaySoundEffect("DoorUnlock");
        }
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void OpenDoor()
    {
        bool temp = CanOpenFromFront;
        CanOpenFromFront = true;
        OpenDoor(true, true);
        CanOpenFromFront = temp;
    }

    public void OpenDoor(bool isOpeningForward = true, bool hasAudio = true)
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
                    anim.SetBool("OpenForward", true);
                else if (!isOpeningForward)
                    anim.SetBool("OpenBackward", true);
                else
                    anim.SetBool("OpenForward", true);
                break;
            case OpenDirect.FORWARD:
                anim.SetBool("OpenBackward", true);
                break;
            case OpenDirect.BACKWARD:
                anim.SetBool("OpenForward", true);
                break;
        }
        CanOpenFromFront = true;
        CanOpenFromBehind = true;

        if (hasAudio)
        {
            audio.PlaySoundEffectDelayed("DoorOpen", 0.05f);
        }
        
        isClosed = false;
    }
    public void CloseDoor(bool hasAudio = true)
    {
        if (anim.transform.localRotation.eulerAngles.y <= 5 || anim.transform.localRotation.eulerAngles.y >= 175)
        {
            anim.SetBool("OpenForward", false);
            anim.SetBool("OpenBackward", false);

            if (hasAudio)
            {
                audio.PlaySoundEffectDelayed("DoorClosed", 0.85f);
            }
                
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
        else if(IsInteractable)
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
