using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class DoorScript : Interactable
{
    public bool isLocked;
    private bool isClosed = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsLocked", isClosed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        if(!isLocked)
        {
            isClosed = !isClosed;
            anim.SetBool("IsLocked", isClosed);
        }
    }
}
