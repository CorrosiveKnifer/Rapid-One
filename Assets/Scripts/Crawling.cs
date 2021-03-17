using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawling : MonoBehaviour
{

    bool IsCrawling = false;
    // Start is called before the first frame update
    void Start()
    {
        //objectPos = adult.transform.localScale;
        //movepos = new Vector3(0.0f, 10.0f, 0.0f);
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
            //Debug.Log("crawl!");
            transform.localScale = new Vector3(1,  0.5f, 1);

        }
        //child view
        //if (Input.GetKeyDown(KeyCode.T))
        else
        {
            //Debug.Log("stand!");
            transform.localScale = new Vector3(1, 1.0f, 1);

        }
    }
}
