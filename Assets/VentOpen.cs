using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpen : Interactable
{
    public override void Activate()
    {
        GetComponent<Animator>().SetTrigger("open");
        Destroy(this);
    }
}
