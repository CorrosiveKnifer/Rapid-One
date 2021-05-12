using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rachael Colaco
/// </summary>
public class ChangeMaterial : MonoBehaviour
{
    
    public Material childMaterial;
    public Material adultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject != null)
            gameObject.SetActive(PlayerController.instance.m_isAdultForm);
    }

    // Update is called once per frame
    void Update()
    {
        //if the object appears in both child and adult
        if (adultMaterial != null && childMaterial != null)
        {
            if (PlayerController.instance.m_isAdultForm)
                gameObject.GetComponent<MeshRenderer>().material = adultMaterial;
            else
                gameObject.GetComponent<MeshRenderer>().material = childMaterial;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

