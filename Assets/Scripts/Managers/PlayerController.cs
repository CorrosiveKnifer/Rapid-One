using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region SingletonImplementation
    public static PlayerController instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [Header("Player references")]
    public Player m_adultForm;
    public Player m_childForm;

    [Header("Player state")]
    public bool m_isAdultForm = true;

    [Header("Player settings")]
    public float m_shiftMaximumDelay = 0.3f;

    private float m_shiftTimeDelay = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_shiftTimeDelay > 0)
        {
            m_shiftTimeDelay = Mathf.Clamp(m_shiftTimeDelay - Time.deltaTime, 0, m_shiftMaximumDelay);
            
        }
        //!CameraController.instance.IsCameraShifting() && 
        if (Input.GetKeyDown(KeyCode.LeftShift) && m_shiftTimeDelay <= 0)
        {
            m_shiftTimeDelay = m_shiftMaximumDelay;
            m_isAdultForm = !m_isAdultForm;
        }

        SetAdultState(m_isAdultForm);
        SetChildState(!m_isAdultForm);
    }

    /*
     * Sets the variables associated to the adult state.
     */
    private void SetAdultState(bool state)
    {
        //Note: By changing the enabled status of the player class,
        //      it is also changing the status of the camera.
        m_adultForm.enabled = state;

        if(state) //While the in adult form...
        {
            //Teleport and rotate the child object for when the player shifts.
            m_childForm.Teleport(m_adultForm.transform.position);
            m_childForm.gameObject.transform.localRotation = m_adultForm.gameObject.transform.localRotation;
            m_childForm.m_currentYRotation = m_adultForm.m_currentYRotation;
        }
    }

    /*
     * Sets the variables associated to the child state.
     */
    private void SetChildState(bool state)
    {
        //Note: By changing the enabled status of the player class,
        //      it is also changing the status of the camera.
        m_childForm.enabled = state;
    }

    public void SetCameraFreeze(bool isCameraFrozen)
    {
        m_adultForm.m_cameraFreeze = isCameraFrozen;
        m_childForm.m_cameraFreeze = isCameraFrozen;
    }
}
