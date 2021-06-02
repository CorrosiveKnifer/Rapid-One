using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEnable : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Outline>().enabled = RayCastToPlayer();
    }

    bool RayCastToPlayer()
    {
        Vector3 direct;
        float dist;
        if (PlayerController.instance.m_isAdultForm)
        {
            direct = PlayerController.instance.m_adultForm.transform.position - transform.position;
            dist = direct.magnitude;

        }
        else
        {
            direct = PlayerController.instance.m_childForm.transform.position - transform.position;
            dist = direct.magnitude;
        }
        foreach (var hit in Physics.RaycastAll(transform.position, direct.normalized, dist))
        {
            if (hit.collider.tag == "Wall")
            {
                return false;
            }
        }
        return true;
    }
}
