using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public Objective[] Objectives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allComplete = true;
        foreach (var obj in Objectives)
        {
            if (!obj.isComplete)
                allComplete = false;
        }

        if (allComplete)
        {
            // show bed objective
        }
        else
        {
            // show other objectives
        }
    }
}
