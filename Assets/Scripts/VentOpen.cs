using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class VentOpen : Interactable
{
    public bool startOpen = false;

    private bool IsOpen = false;
    private void Start()
    {
        if (startOpen)
            Open(false);
    }

    public void Open(bool hasAudio = true)
    {
        if (IsOpen)
            return;

        GetComponent<Animator>().SetBool("Open", true);
        GetComponent<Animator>().SetTrigger("Open");

        if(hasAudio)
        {
            GetComponent<AudioAgent>().PlaySoundEffectDelayed("VentOpen", 0.1f);
        }

        IsOpen = true;
    }
    public void Close(bool hasAudio = true)
    {
        if (!IsOpen)
            return;

        GetComponent<Animator>().SetTrigger("Close");

        if (hasAudio)
        {
            GetComponent<AudioAgent>().PlaySoundEffectDelayed("VentOpen", 1.0f);
        }

        IsOpen = false;
    }

    public override void Activate(Interactor other)
    {
        if(IsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
        base.Activate(other);
    }
}
