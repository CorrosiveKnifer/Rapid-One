using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light light;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Ray[] innerRays = CalculateRays(light.innerSpotAngle);
        Ray[] outerRays = CalculateRays(light.innerSpotAngle + (light.spotAngle - light.innerSpotAngle) / 2);

        //Display Rays for debuging
        float range = light.range;
        for (int i = 0; i < 4; i++)
        {
            Debug.DrawRay(innerRays[i].origin, innerRays[i].direction * range, new Color(1.0f, 0, 0));
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.DrawRay(outerRays[i].origin, outerRays[i].direction * range, new Color(0.5f, 0.1f, 0));
        }

        Debug.DrawRay(transform.position, transform.forward * range, Color.green);
        
        //Raycast to align to a plane
        RaycastHit centreHit;
        //Inner section for hard light
        RaycastHit[] innerHit = new RaycastHit[4];
        Physics.Raycast(transform.position, transform.forward, out centreHit, range);
        for (int i = 0; i < 4; i++)
        {
            Physics.Raycast(innerRays[i].origin, innerRays[i].direction, out innerHit[i], range);
        }
        //Outer section for soft light
        RaycastHit[] outerHit = new RaycastHit[4];
        for (int i = 0; i < 4; i++)
        {
            Physics.Raycast(outerRays[i].origin, outerRays[i].direction, out outerHit[i], range);
        }
        
        //Project the player's position onto the plane created.
        Vector3[] playerProjPoints = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            playerProjPoints[i] = Vector3.Project(player.transform.position - centreHit.point, innerHit[i].point - centreHit.point) + centreHit.point;
            Debug.DrawLine(centreHit.point, playerProjPoints[i], Color.blue);
        }

        HUDScript.instance.SetLight(CalculateLightValue(centreHit, innerHit, outerHit, playerProjPoints));
    }

    private float CalculateLightValue(RaycastHit centreP, RaycastHit[] innerP, RaycastHit[] outerP, Vector3[] projP)
    {
        float[] ratios = new float[4];

        for (int i = 0; i < 4; i++)
        {
            Vector3 OuterLine = outerP[i].point - centreP.point;
            Vector3 innerLine = innerP[i].point - centreP.point;
            Vector3 projLine = projP[i] - centreP.point;
            ratios[i] = Mathf.Clamp(1.0f - (projLine.magnitude - innerLine.magnitude) / (OuterLine.magnitude - innerLine.magnitude), 0.0f, 1.0f);
        }

        float xAxis = Mathf.Max(ratios[0], ratios[1]);
        float yAxis = Mathf.Max(ratios[2], ratios[3]);

        return xAxis * yAxis;
    }

    private Ray[] CalculateRays(float rotatAngle)
    {
        Ray[] output = new Ray[4];

        for (int i = 0; i < 4; i++)
            output[i].origin = transform.position;

        Transform control = new GameObject().transform;
        control.position = transform.position;
        control.forward = transform.forward;

        control.Rotate(new Vector3(-rotatAngle / 2, 0, 0));
        output[0].direction = control.forward;

        control.Rotate(new Vector3(rotatAngle, 0, 0));
        output[1].direction = control.forward;

        control.forward = transform.forward;

        control.Rotate(new Vector3(0, -rotatAngle / 2, 0));
        output[2].direction = control.forward;

        control.Rotate(new Vector3(0, rotatAngle, 0));
        output[3].direction = control.forward;

        Destroy(control.gameObject);
        return output;
    }
}
