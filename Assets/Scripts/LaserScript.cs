using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan, Rachael Colaco
/// </summary>
public class LaserScript : MonoBehaviour
{
    //for the object raycasting laser
    public int reflections = 1;
    public float maxLength = 100;

    public LineRenderer shadowRender;
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private bool isActivated;
    private bool hasShadow = false;
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
        RaycastWithObject(!PlayerController.instance.m_isAdultForm);
    }

    void RaycastWithObject(bool showLaser = true)
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Cast for shadow
        RaycastHit shadowhit;
        if (Physics.Raycast(transform.position, Vector3.down, out shadowhit, 5.0f))
        {
            shadowRender.positionCount = 1;
            shadowRender.SetPosition(0, shadowhit.point);
            hasShadow = true;
        }
        
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                if (hasShadow)
                {
                    Vector3 pos = hit.point;
                    pos.y = shadowhit.point.y;
                    shadowRender.positionCount += 1;
                    shadowRender.SetPosition(shadowRender.positionCount - 1, pos);
                }
                
                float AxisY = ray.direction.y;
                Vector3 ReflectPos = Vector3.Reflect(ray.direction, hit.normal);
                ReflectPos.y = AxisY;
                remainingLength = Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, ReflectPos);
                
                /*
                remainingLength = Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                */
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
                
                if(hasShadow)
                {
                    Vector3 pos = ray.origin + ray.direction * remainingLength;
                    pos.y = shadowhit.point.y;
                    shadowRender.positionCount += 1;
                    shadowRender.SetPosition(shadowRender.positionCount - 1, pos);
                }
                
            }
        }

        lineRenderer.enabled = showLaser;
    }
    
}
