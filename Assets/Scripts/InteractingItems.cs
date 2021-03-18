using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rachael Calaco, Michael Jordan
/// </summary>
public class InteractingItems : MonoBehaviour
{
    public Transform cam;
    public Camera camera;
    private PlayerController playcontr;
    // Start is called before the first frame update
    void Start()
    {
        playcontr = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playcontr.m_bChildForm)
        {
            return;
        }
        //i used F for now
        if (Input.GetKeyDown(KeyCode.E))
        {
            //list of object that get hit by the raycast
            RaycastHit[] hits;

            //to see the line of the raycast
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
            Debug.DrawRay(ray.origin, ray.direction*5.0f, Color.blue, 0.5f);
            hits = Physics.RaycastAll(ray.origin, ray.direction, 5.0f);

            //checking each item
            foreach (var hit in hits)
            {
                hit.collider.gameObject.GetComponentInChildren<Interactable>()?.Activate();
            }
        }
    }
}
