using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public NumberPadScript numberPad;

    public override void Activate()
    {
        numberPad.Show();
    }
}
