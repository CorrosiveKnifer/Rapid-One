using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingItems : MonoBehaviour
{
    public Transform cam;
    public Camera camera;
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
            //Debug.DrawRay(transform.position, cam.forward * 5.0f, Color.blue, 2);
            
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
            Debug.DrawRay(ray.origin, ray.direction*5.0f, Color.blue, 0.5f);
            hits = Physics.RaycastAll(ray.origin, ray.direction, 5.0f);

            //checking each item
            foreach (var hit in hits)
            {
                if(hit.collider.tag == "Wall")
                {
                    return;
                }

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
