using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : MonoBehaviour
{
    public Camera cam;
    public bool isHolding = false;
    private GameObject item;
    public GameObject tempParent;
    Vector3 objectPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //list of object that get hit by the raycast
            RaycastHit[] hits;

            if(item == null)
            {
                //to see the line of the raycast
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
                Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.green, 0.5f);
                hits = Physics.RaycastAll(ray.origin, ray.direction, 5.0f);

                //checking each item
                foreach (var hit in hits)
                {
                    //if the object tag is key to detroy and admit to key
                    if (hit.collider.tag == "Liftable")
                    {
                        item = hit.collider.gameObject;
                        Grabbing();
                        break;
                    }
                }
            }
        }
        else
        {
            Dropping();
        }

        ///checking if it can be held
        if (isHolding == true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
        }
        //if the item isnt null for it (is the isholding is false it gets a error stating the item isnt there for transforming.
        else if(item != null)
        {
            
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
            item = null;
        }
    }

 //getting the grabing function
    void Grabbing()
    {
        Debug.Log("it is grabbed");
        isHolding = true;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;

    }
    void Dropping()
    {
        Debug.Log("it is dropped");
        isHolding = false;
        
    }

    public void Detect()
    {
        Debug.Log("Lifting");
    }



}


