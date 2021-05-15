using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rachael colaco
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    //for the object raycasting laser
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    //for a mirror reflecting laser
    public Transform originalObject;
    public Transform reflectedObject;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.DrawRay(transform.position, Vector3.forward, Color.blue);
        Debug.DrawRay(originalObject.transform.position, Vector3.forward, Color.blue);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.forward);

        }
       */
        RaycastWithObject();
    }

    void RaycastWithObject()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength = Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if(hit.collider.tag == "Player")
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
