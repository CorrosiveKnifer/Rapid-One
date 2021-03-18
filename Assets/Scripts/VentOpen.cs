using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class VentOpen : Interactable
{
    public override void Activate()
    {
        GetComponent<Animator>().SetTrigger("open");
    }

    public void PlayAudio()
    {
        GetComponent<AudioAgent>().PlaySoundEffect("VentOpen");
        Destroy(this);
    }
}
