using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class HandleScript : Interactable
{
    public override void Activate()
    {
        GetComponentInParent<DoorScript>().isLocked = false;
        GetComponentInChildren<Animator>().SetTrigger("Open");
        base.Activate();
    }
}
