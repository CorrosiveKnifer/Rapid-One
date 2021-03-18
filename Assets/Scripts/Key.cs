using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class Key : Interactable
{
    public NumberPadScript numberPad;

    public override void Activate()
    {
        numberPad.Show();
    }
}
