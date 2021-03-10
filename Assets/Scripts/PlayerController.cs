using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_fMouseSensitivity = 100f;
    public Transform m_PlayerBody;

    float m_RotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Don't use please, it's trash
        float mouseX = Input.GetAxis("Mouse X") * m_fMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_fMouseSensitivity * Time.deltaTime;

        //m_RotationX -= mouseY;
        //m_RotationX = Mathf.Clamp(m_RotationX, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(m_RotationX, 0f, 0f);
        //m_PlayerBody.Rotate(Vector3.up * mouseX);

    }
}
