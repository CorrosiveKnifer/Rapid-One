using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Rachael Calaco
/// </summary>
public class Interactable : MonoBehaviour
{
    public UnityEvent m_OnActivate;
    public float m_brainRequirement = 5.0f;

    public virtual void Activate(Interactor other = null)
    {
        m_OnActivate.Invoke();
    }
}
