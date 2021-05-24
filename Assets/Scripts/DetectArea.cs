using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectArea : MonoBehaviour
{
    public GameObject m_adultGameObject = null;
    public GameObject m_childGameObject = null;
    public GameObject HUD;

    public Image AdultIcon;
    public Image ChildIcon;
    
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
        if(other.tag=="Player" && m_adultGameObject != null)
        {
            HUD.GetComponent<MapOfHouse>().playerPic.enabled = false;
            HUD.GetComponent<MapOfHouse>().playerPic = AdultIcon;
            HUD.GetComponent<MapOfHouse>().playerPic.enabled = true;
            //currentPlayerPic.transform.position;
        }
        if(other.tag=="Player" && m_childGameObject != null)
        {
            HUD.GetComponent<MapOfHouse>().playerPic2.enabled = false;
            HUD.GetComponent<MapOfHouse>().playerPic2 = ChildIcon;
            HUD.GetComponent<MapOfHouse>().playerPic2.enabled = true;
            //currentPlayerPic.transform.position;
        }
    }
}
