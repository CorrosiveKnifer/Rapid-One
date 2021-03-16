using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Camera camera;
    public virtual void Detect()
    {
        Debug.Log("No sound Detected");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Detect();
    }

    // Update is called once per frame
    void Update()
    {
        //list of object that get hit by the raycast
        RaycastHit[] hits;

        //to see the line of the raycast
        //Debug.DrawRay(transform.position, cam.forward * 5.0f, Color.blue, 2);

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
        Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.red, 0.5f);
        hits = Physics.RaycastAll(ray.origin, ray.direction, 5.0f);

        //checking each item
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "wall")
            {
                return;
            }

            if (hit.collider.tag == "Liftable")
            {
                //Debug.Log("Lifting");
                gameObject.GetComponent<Liftable>().Detect();
            }
            ///may need to fix this on different tags
            Interactable other = hit.collider.gameObject.GetComponentInChildren<Interactable>();
            if (other != null)
            {
                Debug.Log("interact");
                break;
            }
        }
    }
    
}
