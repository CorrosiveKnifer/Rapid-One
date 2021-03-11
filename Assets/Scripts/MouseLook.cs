using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float m_RotationX = 0f;
    public float m_fMouseSensitivity = 300f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled)
        {
            float mouseY = Input.GetAxis("Mouse Y") * m_fMouseSensitivity * Time.deltaTime;

            m_RotationX -= mouseY;
            m_RotationX = Mathf.Clamp(m_RotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(m_RotationX, 0f, 0f);
        }
    }
}
