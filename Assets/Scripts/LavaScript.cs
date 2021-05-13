using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<PlayerRB>() != null || other.gameObject.GetComponentInParent<PlayerRB>() != null)
        {
            PlayerController.instance.Switch();
        }
    }
}
