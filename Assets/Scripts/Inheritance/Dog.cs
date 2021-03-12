using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Woof");
    }
}
