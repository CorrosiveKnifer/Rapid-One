using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rachael Calaco
/// </summary>
public class Liftable : MonoBehaviour
{
    public float m_myMass = 5.0f;
    private static float raydist = 5.0f;
    public GameObject shadowPrefab;
    private GameObject shadow;

    // Start is called before the first frame update
    void Start()
    {
        shadow = Instantiate(shadowPrefab);
        shadow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, -Vector3.up* raydist, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, -Vector3.up, raydist);

        //checking if there is a hit
        if (hits.Length == 0)
        {
            return;
        }

        RaycastHit closestHit = hits[0];

        //get the closet one
        for (int i = 0; i < hits.Length; i++)
        {
            //hit object doesnt detect itself
            if(hits[i].collider.gameObject == gameObject)
            {
                continue;
            }

            if (closestHit.distance > hits[i].distance)
                closestHit = hits[i];
        }

        ///checking if box is being grabbed
        if(GetComponent<Rigidbody>().useGravity == false)
        {
            if(shadow == null) //if shadow box is deleted
            {
                shadow = Instantiate(shadowPrefab);
            }
            shadow.SetActive(true);

            Debug.Log("in air");
            Debug.Log(closestHit.point);
            shadow.transform.position = new Vector3(transform.position.x, closestHit.point.y, transform.position.z);
        }
        else
        {
            Destroy(shadow);
            shadow = null;
        }
    }
}
