﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float m_mouseSensitivity = 300f; //Mouse Speed
    public float m_movementSpeed = 6.0f; // Move speed
    public float m_gravity = -19.62f;
    public float m_jumpForce = 5.0f;
    public bool m_isChild = false;
    public float m_strength = 10.0f;
    public float m_intellegence = 10.0f;

    public Camera m_myCamera;

    private CharacterController m_charController;
    
    public bool m_bInVents = false;

    private Vector3 m_velocity;
    public bool m_grounded;
    
    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    public bool m_cameraFreeze { get; set; } = false;
    public float m_currentYRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_charController = GetComponent<CharacterController>();

        m_currentYRotation = 0;
        Physics.IgnoreLayerCollision(9, 9);
        if (m_isChild) // Child numbers
        {
            m_movementSpeed = 4.5f;
            m_jumpForce = 6.0f;
        }
        else // Adult numbers
        {
            m_movementSpeed = 6.0f;
            m_jumpForce = 8.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Rotation
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        if(!m_cameraFreeze)
        {
            transform.Rotate(Vector3.up * mouseX);

            m_currentYRotation = Mathf.Clamp(m_currentYRotation - mouseY, -90f, 90f);
            m_myCamera.transform.localRotation = Quaternion.Euler(m_currentYRotation, 0f, 0f);
        }

        // Ground check
        if ((Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask)))
        {
            m_grounded = true;
        }
        else
        {
            m_grounded = false;
        }

        // Snap to ground
        if (m_grounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }
        // Jump

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        // Movement inputs
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        y = (m_grounded && Input.GetButtonDown("Jump")) ? 1.0f : 0.0f;

        if ((x != 0 || z != 0) && m_grounded)
        {
            if (!m_bInVents) // Is not in vents
            {
                if (GetComponent<AudioAgent>().IsAudioStopped("WoodFootsteps"))
                { // Play footsteps
                    if(m_isChild)
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
        m_velocity.y += y * m_jumpForce;
        m_velocity.y += m_gravity * Time.deltaTime;

        // Apply both movement inputs and velocity to the character controller
        m_charController.Move((move * m_movementSpeed + m_velocity) * Time.deltaTime);
    }
    private void OnEnable()
    {
        m_myCamera.enabled = true;
        GetComponent<Interactor>().enabled = true;
    }
    private void OnDisable()
    {
        m_myCamera.enabled = false;
        GetComponent<Interactor>().enabled = false;
    }
    public void Teleport(Vector3 _targetPos)
    {
        m_charController.enabled = false;
        transform.position = _targetPos;
        m_charController.enabled = true;
    }
}
