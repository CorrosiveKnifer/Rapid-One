using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
   // public Transform cam;
    public string Name;

    public virtual void MakeSound()
    {
        Debug.Log("No sound Detected");
    }
    // Start is called before the first frame update
    void Start()
    {
        //MakeSound();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            //list of object that get hit by the raycast
            RaycastHit[] hits;

            //to see the line of the raycast
            Debug.DrawRay(transform.position, cam.forward * 5.0f, Color.blue, 2);
            hits = Physics.RaycastAll(transform.position, cam.forward, 5.0f);

            //checking each item
            foreach (var hit in hits)
            {
                //if the object tag is key to detroy and admit to key
                Animal other = hit.collider.gameObject.GetComponentInChildren<Animal>();
                if (other != null)
                {
                    other.MakeSound();
                    //break;
                }
            }


        }
        */
    }
}
