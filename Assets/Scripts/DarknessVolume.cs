using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessVolume : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerRB>())
        {
            if (other.GetComponent<PlayerRB>().m_isChild /*&& lightVar == 0*/)
                PlayerController.instance.Switch();
        }
    }
}
