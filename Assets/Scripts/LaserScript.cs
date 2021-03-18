using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public LineRenderer ray;
    public float distance = 1.0f;
    public LevelLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, distance);

        if(hits.Length == 0)
        {
            ray.transform.localScale = new Vector3(1.0f, 1.0f, distance + 0.35f);
            return;
        }

        RaycastHit closestHit = hits[0];

        for (int i = 0; i < hits.Length; i++)
        {
            if (closestHit.distance > hits[i].distance)
                closestHit = hits[i];
        }

        ray.transform.localScale = new Vector3(1.0f, 1.0f, closestHit.distance + 0.35f);

        if (closestHit.collider.tag == "Player")
        {
            Debug.Log("Hit the player!");
            GetComponent<AudioAgent>().PlaySoundEffect("Electric_Zap");
            loader.transition.speed = 4f;
            loader.ResetScene();
        }
    }
}
