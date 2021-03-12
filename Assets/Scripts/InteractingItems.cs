using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingItems : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //i used F for now
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
               

                //chekcing and calling the functions to interact with that certain item
                ///NOTE: do change the script of animals into items.
                /*
                Animal other = hit.collider.gameObject.GetComponentInChildren<Animal>();
                if (other != null)
                {
                    other.MakeSound();
                    break;
                }
                */
                Interactable other = hit.collider.gameObject.GetComponentInChildren<Interactable>();
                if (other != null)
                {
                    other.Activate();
                    break;
                }
            }


        }
    }
}
