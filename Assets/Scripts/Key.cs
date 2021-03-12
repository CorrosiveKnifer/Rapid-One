using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public override void Activate()
    {
        Debug.Log("Key acquired");
    }
}
