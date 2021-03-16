using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveRadius : MonoBehaviour
{
    float m_DetectionRadius = 0.0f;
    public LevelLoader m_LevelLoader;
    public Transform m_PlayerTransform;
    public Player m_Player;
    // Start is called before the first frame update
    void Start()
    {
        m_DetectionRadius = (transform.localScale.x) * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(m_PlayerTransform.position.x, m_PlayerTransform.position.z)) < m_DetectionRadius)
        {
            if (!m_Player.m_bIsChild)
            {
                m_LevelLoader.LoadNextLevel();
            }
        }
    }
}
