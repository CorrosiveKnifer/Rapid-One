using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Color myColor;
    public Texture[] m_DigitTextures;
    public uint myDigit;
    void Awake()
    {
        myDigit = (uint)(Random.Range(0, 10));

        foreach (var item in GetComponentsInParent<MeshRenderer>())
        {
            item.material.mainTexture = m_DigitTextures[myDigit];
        }
        GetComponent<MeshRenderer>().material.color = myColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
