using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class ObjectSync : MonoBehaviour
{
    public GameObject AdultObject;
    public GameObject ChildObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check which object is active.
        if (AdultObject.activeSelf)
        {
            ChildObject.transform.position = AdultObject.transform.position;
            ChildObject.transform.rotation = AdultObject.transform.rotation;
        }
        else if (ChildObject.activeSelf)
        {

            AdultObject.transform.position = ChildObject.transform.position;
            AdultObject.transform.rotation = ChildObject.transform.rotation;
        }
    }
}
