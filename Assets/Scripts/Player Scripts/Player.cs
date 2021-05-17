using System.Collections;
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
    public bool m_bIsChild = false;
    public float m_strength = 10.0f;
    public float m_intellegence = 10.0f;

    public Camera m_myCamera;

    public CharacterController m_CharController;
    
    public bool m_bInVents = false;

    private Vector3 m_vVelocity;
    public bool m_bGrounded;
    
    public Transform m_GroundCheck;
    public float m_GroundDistance = 0.4f;
    public LayerMask m_GroundMask;

    public bool m_cameraFreeze { get; set; } = false;
    public float m_currentYRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_currentYRotation = 0;
        Physics.IgnoreLayerCollision(9, 9);
        if (m_bIsChild) // Child numbers
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
        //Mouse Rotation

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
        float y = 0.0f;
        float z = 0.0f;

        // Movement inputs
        //if (!CameraController.instance.IsCameraShifting())
        //{
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            y = (m_bGrounded && Input.GetButtonDown("Jump")) ? 1.0f : 0.0f;
        //}

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
        m_vVelocity.y += y * m_jumpForce;
        m_vVelocity.y += m_gravity * Time.deltaTime;

        // Apply both movement inputs and velocity to the character controller
        m_CharController.Move((move * m_movementSpeed + m_vVelocity) * Time.deltaTime);
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
        m_CharController.enabled = false;
        transform.position = _targetPos;
        m_CharController.enabled = true;
    }
}
