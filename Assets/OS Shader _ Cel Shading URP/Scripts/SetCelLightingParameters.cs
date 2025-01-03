﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SetCelLightingParameters : MonoBehaviour
{

    Light mainLight;

    [SerializeField, ColorUsage(false, true)]
    Color ambientLight = Color.white;

    private void Start()
    {
        mainLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_LIGHTINTENSITY", mainLight.intensity);
        Shader.SetGlobalColor("_AMBIENTLIGHT", ambientLight);
    }
}
