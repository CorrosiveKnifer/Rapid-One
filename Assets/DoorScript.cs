using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    public bool isLocked;
    private bool isClosed = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsLocked", isClosed);
    }

    public override void Activate()
    {
        if(!isLocked)
        {
            isClosed = !isClosed;
        }
    }
}
