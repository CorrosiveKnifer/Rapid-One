using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ChildObject.transform.position = AdultObject.transform.position;
        ChildObject.transform.rotation = AdultObject.transform.rotation;
    }
}
