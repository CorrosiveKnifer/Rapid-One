using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    PlayerController m_target;
    PlayerRB m_childForm;
    public float m_damageMult = 1.0f;

    private bool m_isDisabled = false;

    public PressurePlate m_pressurePlate;
    Quaternion m_startRot;

    // Start is called before the first frame update
    void Start()
    {
        m_target = PlayerController.instance;
        m_childForm = m_target.m_childForm;
        m_startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null)
            return;

        if (m_pressurePlate != null)
        {
            m_isDisabled = !m_pressurePlate.IsActive();
        }

        if (!m_target.m_isAdultForm && !m_isDisabled)
        {
            Vector3 direction = m_childForm.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 0.1f);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, 10.0f);

            if (hits.Length != 0)
            {
                RaycastHit closestHit = hits[0];

                for (int i = 0; i < hits.Length; i++)
                {
                    if (closestHit.distance > hits[i].distance)
                        closestHit = hits[i];
                }

                if (closestHit.collider.GetComponent<PlayerRB>())
                {
                    if (closestHit.collider.GetComponent<PlayerRB>().m_isChild)
                    {
                        // Add damage to player
                        HUDScript.instance.ApplyDamage(Time.deltaTime * m_damageMult);
                    }
                }
            }
        }
        else
        {
            transform.rotation = m_startRot;
        }
    }
}
