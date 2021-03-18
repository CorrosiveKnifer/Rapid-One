using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rachael Calaco
/// </summary>
public class Liftable : MonoBehaviour
{
    public Camera cam;
    public bool isHolding = false;
    private GameObject item;
    private Transform itemParent;
    public GameObject tempParent;

    private PlayerController playcontr;
    public GameObject HUDObject;

    Vector3 objectPos;

    private HUDScript HUD;
    // Start is called before the first frame update
    void Start()
    {
        HUD = HUDObject.GetComponent<HUDScript>();
        playcontr = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playcontr.m_bChildForm)
        {
            if(item != null)
            {
                Dropping();
                objectPos = item.transform.position;
                item.transform.parent = itemParent;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.transform.position = objectPos;
                itemParent = null;
                item = null;
            }
            return;
        }

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

                if (hits.Length == 0)
                {
                    return;
                }

                RaycastHit closestHit = hits[0];

                for (int i = 0; i < hits.Length; i++)
                {
                    if (closestHit.distance > hits[i].distance)
                        closestHit = hits[i];
                }

                //if the object tag is key to detroy and admit to key
                GroundCheck lift = closestHit.collider.gameObject.GetComponentInChildren<GroundCheck>();
                if (lift != null)
                {
                    item = closestHit.collider.gameObject;
                    itemParent = item.transform.parent;
                    Grabbing();
                }
            }
        }
        else
        {
            Dropping();
        }

        if(HUD != null)
            HUD.isHandOpen = !isHolding;

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
            item.transform.parent = itemParent;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
            itemParent = null;
            item = null;
        }
    }

 //getting the grabing function
    void Grabbing()
    {
        isHolding = true;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;

    }
    void Dropping()
    {
        isHolding = false;
        
    }

    public void Detect()
    {
        //Debug.Log("Lifting");
    }



}


