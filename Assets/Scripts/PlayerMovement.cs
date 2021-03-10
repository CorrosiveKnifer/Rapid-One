using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_bIsChild = false;

    public CharacterController m_CharController;
    public float m_fSpeed = 12f; // Move speed
    public float m_fGravity = -9.81f;

    public Vector3 m_vVelocity;
    public bool m_bGrounded;
    float m_fJumpThrust = 5.0f;

    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    // Start is called before the first frame update
    void Start()
    {
        if (m_bIsChild) // Child numbers
        {
            m_fSpeed = 3.0f;
            m_fJumpThrust = 5.0f;
        }
        else // Adult numbers
        {
            m_fSpeed = 6.0f;
            m_fJumpThrust = 8.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        if ((Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask)))
        {
            m_bGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            m_bGrounded = false;
            Debug.Log("Not Grounded");
        }

        // Snap to ground
        if (m_bGrounded && m_vVelocity.y < 0)
        {
            m_vVelocity.y = -2f;
        }
        // Jump
        if (m_bGrounded && Input.GetButtonDown("Jump"))
        {
            m_vVelocity.y = m_fJumpThrust;
        }

        // Movement inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Create vector from player's current orientation (meaning it will work with rotating camera)
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply gravity to velocity
        m_vVelocity.y += m_fGravity * Time.deltaTime;

        // Apply both movement inputs and velocity to the character controller
        m_CharController.Move((move * m_fSpeed + m_vVelocity) * Time.deltaTime);
    }
}
