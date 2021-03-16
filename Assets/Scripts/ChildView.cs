﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildView : MonoBehaviour
{
   
    public GameObject[] childs; //the list of objects relating for the child to see
    public GameObject[] adults; //the list of objects relating for the adult to see
    private bool isAdult = true;

    public float transitionDelay = 0.3f;
    private float delay = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (childs.Length == 0)
        {
            childs = GameObject.FindGameObjectsWithTag("ChildObjects");

        }
        if (adults.Length == 0)
        {
            adults = GameObject.FindGameObjectsWithTag("AdultObjects");

        }

        foreach (GameObject child in childs)
        {
            
            child.SetActive(false);

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay = Mathf.Clamp(delay - Time.deltaTime, 0, transitionDelay);
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift) && delay == 0)
        //{
        //    delay = transitionDelay;
        //    isAdult = !(isAdult);
        //}
            //adult view
        //if (Input.GetKeyDown(KeyCode.Y))
        if (CameraController.instance.agent.currentState == CameraAgent.AgentState.FOLLOW_ADULT)
        {
            
            //checking through child object and makes them dissapear
            foreach (GameObject child in childs)
            {
                Debug.Log("Dissapear!");
                child.SetActive(false);

            }
            //checking through adult object and makes them appear
            foreach (GameObject adult in adults)
            {
                
                adult.SetActive(true);
            }
        }
        //child view
        //if (Input.GetKeyDown(KeyCode.T))
        else
        {
            //checking through child object and makes them appear
            foreach (GameObject child in childs)
            {
                Debug.Log("Appear!");
                child.SetActive(true);
            }

            //checking through adult object and makes them dissapear
            foreach (GameObject adult in adults)
            {
               
                adult.SetActive(false);
            }
        }
    }
}
