using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKey : MonoBehaviour
{

    public Transform cam;
    bool hasKey = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //list of object that get hit by the raycast
            RaycastHit[] hits;

            //to see the line of the raycast
            Debug.DrawRay(transform.position, cam.forward * 5.0f, Color.red, 2);
            hits = Physics.RaycastAll(transform.position, cam.forward, 5.0f);

            //checking each item
            foreach (var hit in hits)
            {
                //if the object tag is key to detroy and admit to key
                if(hit.collider.tag=="Key")
                {
                    Destroy(hit.collider.gameObject);
                    hasKey = true;
                    break;
                }
            }
            
            
        }
    }
    
}
