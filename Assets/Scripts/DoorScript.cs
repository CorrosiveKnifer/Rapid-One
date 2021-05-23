using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class DoorScript : Interactable
{
    public uint KeyID = 0;
    public bool isLocked = false;
    public bool CanOpenFromFront = true;
    public bool CanOpenFromBehind = true;

    private bool isClosed = true;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isClosed = isLocked;
        anim.SetBool("IsLocked", isClosed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor(bool isOpeningForward = true)
    {
        isClosed = false;
        anim.SetBool("IsLocked", isClosed);
    }

    public void CloseDoor()
    {
        isClosed = true;
        anim.SetBool("IsLocked", isClosed);
    }

    public override void Activate(Interactor other)
    {
        if(other != null)
        {
            var direct = transform.position - other.transform.position;
            var dot = Vector3.Dot(direct, other.transform.right);
            
            if(dot < 0) //Behind the door
            {
                anim.SetTrigger("OpenBackward");
            }
            if (dot < 0) //Infront of the door
            {
                if(isClosed)
                    anim.SetTrigger("OpenForwards");
            }

            isClosed = !isClosed;
        }
        //if(!isLocked)
        //{
        //    isClosed = !isClosed;
        //    anim.SetBool("IsLocked", isClosed);
        //}
        //else
        //{
        //    base.Activate(other);
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<KeyScript>() != null)
        {
            if(other.gameObject.GetComponent<KeyScript>().keyID == KeyID)
            {
                Destroy(other.gameObject);
                isLocked = false;
                //Activate();
            }
        }
    }
}
