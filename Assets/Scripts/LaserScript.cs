﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan, Rachael Colaco
/// </summary>
public class LaserScript : MonoBehaviour
{
    //for the object raycasting laser
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private bool isActivated;
    void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    //public LineRenderer ray;
    //public float distance = 1.0f;
    //public LevelLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastWithObject();
    }
    /*
    void shootingLaser()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, distance);

        if (hits.Length == 0)
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
            PlayerController.instance.Switch();
            //loader.transition.speed = 4f;
            //loader.ResetScene();
        }
    }
    */
    void RaycastWithObject()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength = Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                //for laser reactants---------------
                if (hit.collider.gameObject.GetComponent<LaserReactant>() != null)
                {
                    //Debug.Log("Door is Open");
                    hit.collider.GetComponent<LaserReactant>().IsActivated = true;
                    break;
                }
                //------------------
                if (hit.collider.tag == "Player" && !PlayerController.instance.m_isAdultForm)
                {

                    GetComponent<AudioAgent>().PlaySoundEffect("Electric_Zap");
                    PlayerController.instance.Switch();
                    break;
                }
                if (hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
    
}
