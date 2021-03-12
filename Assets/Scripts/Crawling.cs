using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawling : MonoBehaviour
{
    public GameObject adult;
    Vector3 objectPos;
    Vector3 movepos;
    bool IsCrawling = false;
    // Start is called before the first frame update
    void Start()
    {
        objectPos = adult.transform.position;
        movepos = new Vector3(0.0f, 10.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            IsCrawling = !(IsCrawling);
        }
        if (IsCrawling == true)
        {
            Debug.Log("crawl!");
            transform.position = objectPos + movepos;

        }
        //child view
        //if (Input.GetKeyDown(KeyCode.T))
        else
        {
            Debug.Log("stand!");
            adult.transform.position = objectPos;

        }
    }
}
