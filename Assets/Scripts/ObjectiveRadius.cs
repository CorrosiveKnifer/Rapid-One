using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class ObjectiveRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" && PlayerController.instance.m_isAdultForm)
        {
            LevelLoader.GetInstance().LoadNextLevel();
        }
    }
}
