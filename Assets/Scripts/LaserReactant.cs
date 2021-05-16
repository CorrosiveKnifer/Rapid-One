using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReactant : MonoBehaviour
{
    public UnityEvent m_OnPower;
    public UnityEvent m_OffPower;

    public bool IsActivated = false;
    public bool IsPowered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool currentpower = IsPowered;
        IsPowered = IsActivated;
        if (currentpower != IsPowered)
        {
            if (IsPowered)
            {
                m_OnPower.Invoke();
                Debug.Log("puzzle Open");
            }
            else
            {
                m_OffPower.Invoke();
            }
        }
        if (IsActivated)
        {
            IsActivated = false;
        }

    }
    public void Activated()
    {
        IsActivated = true;
    }
 
}
