using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan, William de Beer
/// </summary>
public class VentEventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] detectables;
    public bool isDependableIntersecting { get; private set; }

    private BoxCollider[] colliders;

    void Start()
    {
        colliders = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = 0; j < detectables.Length; j++)
            {
                Collider[] others = detectables[j].GetComponentsInChildren<Collider>();
                foreach (var other in others)
                {
                    isDependableIntersecting = colliders[i].bounds.Intersects(other.bounds);
                    if (isDependableIntersecting)
                    {
                        detectables[j].GetComponent<Player>().m_bInVents = true;
                        return;
                    }
                }
            }
        }
        detectables[0].GetComponent<Player>().m_bInVents = false;
    }
}
