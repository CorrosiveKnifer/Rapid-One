using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_fMouseSensitivity = 300f;
    public Transform m_AdultBody;
    public Transform m_ChildBody;

    public Camera m_AdultCamera;
    public Camera m_ChildCamera;

    public Player Adult;
    public Player Child;

    bool m_bChildForm = false;

    float m_RotationX = 0f;
    bool m_bCamMovement = true;

    public float transitionDelay = 1.5f;
    private float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_AdultCamera.enabled = true;
        m_ChildCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_fMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_fMouseSensitivity * Time.deltaTime;

        if (m_bCamMovement)
        {
            m_ChildBody.Rotate(Vector3.up * mouseX);
            if (!m_bChildForm)
            {
                m_AdultBody.Rotate(Vector3.up * mouseX);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (m_bCamMovement)
            {
                DisableCameraMovement();
            }
            else
            {
                EnableCameraMovement();
            }
        }

        if (delay > 0)
        {
            delay = Mathf.Clamp(delay - Time.deltaTime, 0, transitionDelay);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && delay == 0 && !CameraController.instance.IsCameraShifting())
        {
            delay = transitionDelay;
            ToggleControlChild();
        }

        if (!m_bChildForm)
        {
            Child.Teleport(m_AdultBody.position);
        }
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
            //Child.Teleport(m_AdultBody.position);
            // Enable child control
            Child.EnableControl();

            m_AdultCamera.enabled = false;
            m_ChildCamera.enabled = true;
        }
        else
        {
            // Disable child control
            Child.DisableControl();

            m_AdultBody.rotation = m_ChildBody.rotation;

            // Enable adult control
            Adult.EnableControl();

            m_AdultCamera.enabled = true;
            m_ChildCamera.enabled = false;
        }
    }

    void EnableCameraMovement()
    {
        m_AdultCamera.GetComponent<MouseLook>().m_bMovementEnabled = true;
        m_ChildCamera.GetComponent<MouseLook>().m_bMovementEnabled = true;
        m_bCamMovement = true;
    }

    void DisableCameraMovement()
    {
        m_AdultCamera.GetComponent<MouseLook>().m_bMovementEnabled = false;
        m_ChildCamera.GetComponent<MouseLook>().m_bMovementEnabled = false;
        m_bCamMovement = false;
    }
}
