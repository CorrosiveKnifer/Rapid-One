using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapOfHouse : MonoBehaviour
{
    public GameObject map;
    public Image playerPic;
    // Start is called before the first frame update
    void Start()
    {
        map.SetActive(false);
        playerPic.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            map.SetActive(!map.activeSelf);

        }
        if (Input.GetKeyDown("r"))
        {
            map.SetActive(false);
        }
    }

}
