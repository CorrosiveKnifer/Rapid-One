using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuScreen : MonoBehaviour
{
    public GameObject pMenu;

    // Start is called before the first frame update
    void Start()
    {
        pMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            pMenu.SetActive(!pMenu.activeSelf);        
        }

        if (pMenu.activeSelf)
        {
            PlayerController.instance.SetCameraFreeze(true); 
            Time.timeScale = 0;
              
        }
        else
        {
            PlayerController.instance.SetCameraFreeze(false); 
            Time.timeScale = 1;
        }
    }
}