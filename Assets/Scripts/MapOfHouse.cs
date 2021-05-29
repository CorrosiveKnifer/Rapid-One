using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapOfHouse : MonoBehaviour
{
    public GameObject map;
    public Image playerPic;
    public Image playerPic2;
    // Start is called before the first frame update
    void Start()
    {
        map.SetActive(false);
        playerPic.enabled = true;
        playerPic.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            map.SetActive(!map.activeSelf);

            if (map.activeSelf)
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
}
