using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerRB>() != null || other.gameObject.GetComponentInParent<PlayerRB>() != null)
        {
            HUDScript.instance.ApplyDamage(5.0f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<PlayerRB>() != null || other.gameObject.GetComponentInParent<PlayerRB>() != null)
        {
            HUDScript.instance.ApplyDamage(5.0f * Time.deltaTime);
        }
    }
}
