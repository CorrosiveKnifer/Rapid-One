using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// William de Beer
/// </summary>
public class ObjectiveRadius : MonoBehaviour
{
    float m_DetectionRadius = 0.0f;
    public Transform m_PlayerTransform;
    public PlayerRB m_Player;
    // Start is called before the first frame update
    void Start()
    {
        m_DetectionRadius = (transform.localScale.x) * 5;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(m_PlayerTransform.position.x, m_PlayerTransform.position.z)) < m_DetectionRadius)
        {
            if (!m_Player.m_isChild)
            {
                LevelLoader.GetInstance().LoadNextLevel();
            }
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" && !m_Player.m_isChild)
        {
            m_LevelLoader.LoadNextLevel();
        }
    }
}
