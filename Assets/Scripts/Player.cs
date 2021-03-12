using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool m_bIsChild = false;
    bool m_bIsEnabled;

    public NumberPadScript test;

    public CharacterController m_CharController;
    public float m_fSpeed = 12f; // Move speed
    float m_fGravity = -19.62f;

    public Vector3 m_vVelocity;
    public bool m_bGrounded;
    float m_fJumpThrust = 5.0f;

    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    bool m_bTeleportQueued = false;
    Vector3 m_TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);

        if (m_bIsChild) // Child numbers
        {
            m_bIsEnabled = false;
            m_fSpeed = 3.0f;
            m_fJumpThrust = 5.0f;
        }
        else // Adult numbers
        {
            m_bIsEnabled = true;
            m_fSpeed = 6.0f;
            m_fJumpThrust = 8.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            test?.Show();
        }

        // Ground check
        if ((Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask)))
        {
            m_bGrounded = true;
            //Debug.Log("Grounded");
        }
        else
        {
            m_bGrounded = false;
           // Debug.Log("Not Grounded");
        }

        // Snap to ground
        if (m_bGrounded && m_vVelocity.y < 0)
        {
            m_vVelocity.y = -2f;
        }
        // Jump

        float x = 0.0f;
        float z = 0.0f;

        // Movement inputs
        if (m_bIsEnabled)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            if (m_bGrounded && Input.GetButtonDown("Jump"))
            {
                m_vVelocity.y = m_fJumpThrust;
            }
        }
        // Create vector from player's current orientation (meaning it will work with rotating camera)
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply gravity to velocityS
        m_vVelocity.y += m_fGravity * Time.deltaTime;

        // Apply both movement inputs and velocity to the character controller
        m_CharController.Move((move * m_fSpeed + m_vVelocity) * Time.deltaTime);
    }

    private void LateUpdate()
    {
        //if (m_bTeleportQueued)
        //{
        //    transform.position = m_TargetPos;
        //    m_bTeleportQueued = false;
        //}
    }
    public void EnableControl()
    {
        m_bIsEnabled = true;
    }

    public void DisableControl()
    {
        m_bIsEnabled = false;
    }

    public void Teleport(Vector3 _targetPos)
    {
        Debug.Log(_targetPos);
        m_TargetPos = _targetPos;
        //m_bTeleportQueued = true;
        m_CharController.enabled = false;
        transform.position = new Vector3(_targetPos.x, _targetPos.y, _targetPos.z);
        m_CharController.enabled = true;
        //m_CharController.Move(new Vector3(_targetPos.x, _targetPos.y, _targetPos.z) - transform.position);
    }

}
