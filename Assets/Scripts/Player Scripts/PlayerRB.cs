﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class PlayerRB : MonoBehaviour
{
    [Header("Player Settings")]
    public float m_mouseSensitivity = 300f; //Mouse Speed
    public float m_movementSpeed; // Move speed
    public float m_gravity = -1.0f;
    public float m_jumpForce = 500.0f;
    public bool m_isChild = false;
    public float m_strength = 10.0f;
    public float m_intellegence = 10.0f;

    public Camera m_myCamera;

    private Rigidbody m_rigidBody;
    
    public bool m_bInVents = false;

    private Vector3 m_velocity;
    public bool m_grounded;
    
    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    public bool m_cameraFreeze { get; set; } = false;
    public float m_currentYRotation;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

        m_currentYRotation = 0;
        Physics.IgnoreLayerCollision(9, 9);
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Rotation
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        m_cameraFreeze = Input.GetMouseButton(0) && Input.GetMouseButton(1);

        if (!m_cameraFreeze)
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

        //// Snap to ground
        //if (m_grounded && m_velocity.y < 0)
        //{
        //    m_velocity.y = -2f;
        //}
        // Jump

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        // Movement inputs
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        //y = (m_grounded && Input.GetButtonDown("Jump")) ? 1.0f : 0.0f;

        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            m_velocity.y = m_jumpForce;
        }

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


        //m_velocity.y += y * m_jumpForce;

        //if (m_grounded && m_velocity.y < 0)
        //{
        //    m_velocity.y = -0.5f;
        //}
        //else
        //{
        //    m_velocity.y += m_gravity * Time.fixedDeltaTime;
        //}

        // Apply to velocity rigidbody
        m_rigidBody.velocity = new Vector3(move.normalized.x * m_movementSpeed, 0, move.normalized.z * m_movementSpeed);
    }

    private void FixedUpdate()
    {
        if (m_grounded && m_velocity.y < 0)
        {
            m_velocity.y = -0.05f;
        }
        else
        {
            m_velocity.y += m_gravity * Time.fixedDeltaTime;
        }
        transform.position += new Vector3(0, m_velocity.y, 0);
    }
    private void OnEnable()
    {
        m_myCamera.enabled = true;
        GetComponent<Interactor>().enabled = true;
        m_rigidBody.velocity = Vector3.zero;
    }
    private void OnDisable()
    {
        m_myCamera.enabled = false;
        GetComponent<Interactor>().enabled = false;
        m_rigidBody.velocity = Vector3.zero;
    }
    public void Teleport(Vector3 _targetPos)
    {
        transform.position = _targetPos;
        m_rigidBody.velocity = Vector3.zero;
    }
}
