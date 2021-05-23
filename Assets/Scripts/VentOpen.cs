using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class VentOpen : Interactable
{
    private void Start()
    {
        m_brainRequirement = 5.0f;
    }
    public override void Activate(Interactor other)
    {
        GetComponent<Animator>().SetTrigger("open");
    }

    public void PlayAudio()
    {
        GetComponent<AudioAgent>().PlaySoundEffect("VentOpen");
        Destroy(this);
    }
}
