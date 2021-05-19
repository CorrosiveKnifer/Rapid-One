using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanVolume : MonoBehaviour
{
    public float m_fanPower = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerRB>())
        {
            if (other.GetComponent<PlayerRB>().m_isChild)
                other.GetComponent<Rigidbody>().AddForce(transform.up * m_fanPower, ForceMode.Acceleration);
        }
    }
}
