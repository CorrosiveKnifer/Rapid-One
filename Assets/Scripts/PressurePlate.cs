using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// William de Beer
/// </summary>
public class PressurePlate : MonoBehaviour
{
    private Collider m_detectionVolume;
    public float m_totalMass;
    public float m_weightThreshold = 1.0f;
    List<Liftable> m_liftables;

    // Start is called before the first frame update
    void Start()
    {
        m_totalMass = 0.0f;
        m_liftables = new List<Liftable>();
    }

    // Update is called once per frame
    void Update()
    {
        float totalMass = 0;
        foreach (var liftable in m_liftables)
        {
            if (liftable.GetComponent<Rigidbody>().useGravity == true &&
                liftable.GetComponent<Rigidbody>().velocity.y <= 0.01f &&
                liftable.GetComponent<Rigidbody>().velocity.y >= -0.05f)
            {
                totalMass += liftable.GetComponent<Rigidbody>().mass;
            }
        }
        m_totalMass = totalMass;

        if (IsActive())
            GetComponent<MeshRenderer>().material.color = Color.green;
        else
            GetComponent<MeshRenderer>().material.color = Color.red;

        Debug.Log(m_totalMass);
    }

    public bool IsActive()
    {
        return (m_totalMass >= m_weightThreshold);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Liftable>())
        {
            m_liftables.Add(other.GetComponent<Liftable>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Liftable>() && m_liftables.Contains(other.GetComponent<Liftable>()))
        {
            m_liftables.Remove(other.GetComponent<Liftable>());
        }
    }
}
