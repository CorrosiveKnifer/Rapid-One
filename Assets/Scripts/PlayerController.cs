using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_fMouseSensitivity = 100f;
    public Transform m_AdultBody;
    public Transform m_ChildBody;

    public Player Adult;
    public Player Child;

    bool m_bChildForm = false;

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


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleControlChild();
        }



            //m_RotationX -= mouseY;
            //m_RotationX = Mathf.Clamp(m_RotationX, -90f, 90f);

            //transform.localRotation = Quaternion.Euler(m_RotationX, 0f, 0f);
            //m_PlayerBody.Rotate(Vector3.up * mouseX);

    }


    void ToggleControlChild()
    {
        m_bChildForm = !m_bChildForm;

        if (m_bChildForm)
        {
            // Disable adult control
            Adult.DisableControl();

            // Teleport child to adult position
            //m_ChildBody.position.Set(m_AdultBody.position.x, m_AdultBody.position.y, m_AdultBody.position.z);
            //m_ChildTransform.position = m_AdultBody.position;
            Child.Teleport(m_AdultBody.position);
            // Enable child control
            Child.EnableControl();
        }
        else
        {
            // Disable child control
            Child.DisableControl();

            // Enable adult control
            Adult.EnableControl();
        }
    }
}
