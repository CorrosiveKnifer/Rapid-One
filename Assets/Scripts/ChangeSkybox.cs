using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rachael Colaco
/// </summary>
public class ChangeSkybox : MonoBehaviour
{
   
    public Material childMaterial;
    public Material adultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = adultMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //if the object appears in both child and adult
        if (adultMaterial != null && childMaterial != null)
        {
            if (PlayerController.instance.m_isAdultForm)
                RenderSettings.skybox = adultMaterial;
            else
                RenderSettings.skybox = childMaterial;
        }

    }
}
