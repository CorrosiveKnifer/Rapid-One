using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public bool m_bIsChild = false;
    bool m_bIsEnabled;

    public CharacterController m_CharController;
    public float m_fSpeed = 12f; // Move speed
    float m_fGravity = -19.62f;

    public bool m_bInVents = false;

    public Vector3 m_vVelocity;
    public bool m_bGrounded;
    float m_fJumpThrust = 5.0f;

    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);

        if (m_bIsChild) // Child numbers
        {
            m_bIsEnabled = false;
            m_fSpeed = 4.5f;
            m_fJumpThrust = 6.0f;
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
        // Ground check
        if ((Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask)))
        {
            m_bGrounded = true;
        }
        else
        {
            m_bGrounded = false;
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
        if (m_bIsEnabled && !CameraController.instance.IsCameraShifting())
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");


            if (m_bGrounded && Input.GetButtonDown("Jump"))
            {
                m_vVelocity.y = m_fJumpThrust;
            }
        }

        if ((x != 0 || z != 0) && m_bGrounded)
        {
            if (!m_bInVents) // Is not in vents
            {
                if (GetComponent<AudioAgent>().IsAudioStopped("WoodFootsteps"))
                { // Play footsteps
                    if(m_bIsChild)
                    {
                        GetComponent<AudioAgent>().PlaySoundEffect("WoodFootsteps", false, 255, 1.5f);
                    }
                    else
                    {
                        GetComponent<AudioAgent>().PlaySoundEffect("WoodFootsteps");
                    }
                }
                if (!GetComponent<AudioAgent>().IsAudioStopped("MetalFootsteps"))
                { // Stop metal foot steps if still playing
                    GetComponent<AudioAgent>().StopAudio("MetalFootsteps");
                }
            }
            else
            {
                if (GetComponent<AudioAgent>().IsAudioStopped("MetalFootsteps"))
                { // Play metal footsteps
                    GetComponent<AudioAgent>().PlaySoundEffect("MetalFootsteps");
                }
                if (!GetComponent<AudioAgent>().IsAudioStopped("WoodFootsteps"))
                { // Stop normal footsteps if still playing
                    GetComponent<AudioAgent>().StopAudio("WoodFootsteps");
                }
                Debug.Log("Player In Vents");
            }
        }
        else
        {
            if (!GetComponent<AudioAgent>().IsAudioStopped("WoodFootsteps"))
            {
                GetComponent<AudioAgent>().StopAudio("WoodFootsteps");
            }
            if (!GetComponent<AudioAgent>().IsAudioStopped("MetalFootsteps"))
            {
                GetComponent<AudioAgent>().StopAudio("MetalFootsteps");
            }

        }

        // Create vector from player's current orientation (meaning it will work with rotating camera)
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply gravity to velocityS
        m_vVelocity.y += m_fGravity * Time.deltaTime;

        // Apply both movement inputs and velocity to the character controller
        m_CharController.Move((move * m_fSpeed + m_vVelocity) * Time.deltaTime);
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
        m_CharController.enabled = false;
        transform.position = _targetPos;
        m_CharController.enabled = true;
    }

}
