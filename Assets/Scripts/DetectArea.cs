using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectArea : MonoBehaviour
{

    public Image currentPlayerPic;
    public GameObject HUB;
    // Start is called before the first frame update
    void Start()
    {
        //currentPlayerPic.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            HUB.GetComponent<MapOfHouse>().playerPic.enabled = false;
            HUB.GetComponent<MapOfHouse>().playerPic = currentPlayerPic;
            HUB.GetComponent<MapOfHouse>().playerPic.enabled = true;
            //currentPlayerPic.transform.position;
        }
    }
}
