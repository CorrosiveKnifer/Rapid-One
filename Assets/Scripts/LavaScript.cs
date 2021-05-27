﻿using System.Collections;
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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<PlayerRB>() != null || other.gameObject.GetComponentInParent<PlayerRB>() != null)
        {;
            other.gameObject.GetComponentInChildren<PlayerRB>().m_canJump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerRB>() != null || other.gameObject.GetComponentInParent<PlayerRB>() != null)
        {
            HUDScript.instance.ApplyDamage(5.0f * Time.deltaTime);
            other.gameObject.GetComponentInChildren<PlayerRB>().m_canJump = false;
        }
    }
}
