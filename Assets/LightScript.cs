using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Rigidbody myRigidbody;
    private Light light;
    private GameObject player;
    private PlayerRB currentPlayer;

    private Vector3 centrePos;
    private Vector3[] innerPos;
    private Vector3[] outerPos;

    private bool hasBaked = false;
    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light>();
        innerPos = new Vector3[4];
        outerPos = new Vector3[4];

        BakeInformation();
        hasBaked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.m_isAdultForm)
        {
            player = PlayerController.instance.m_adultForm.gameObject;
        }
        else
        {
            player = PlayerController.instance.m_childForm.gameObject;
        }

        if (myRigidbody != null && myRigidbody.useGravity && myRigidbody.velocity.magnitude <= 0.5f)
        {
            if(!hasBaked)
            {
                BakeInformation();
                hasBaked = true;
            }
        }
        else if(myRigidbody != null)
        {
            hasBaked = false;
        }

        foreach (var item in outerPos)
        {
            Debug.DrawLine(centrePos, item, Color.green);
        }

        light.enabled = Vector3.Distance(transform.position, player.transform.position) <= 15.0f;

        //Project the player's position onto the plane created.
        Vector3[] playerProjPoints = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            playerProjPoints[i] = Vector3.Project(player.transform.position - centrePos, innerPos[i] - centrePos) + centrePos;
            Debug.DrawLine(centrePos, playerProjPoints[i], Color.blue, 0.1f);
        }

        //HUDScript.instance.SetLight(CalculateLightValue(playerProjPoints));
    }
    private void BakeInformation()
    {
        Ray[] innerRays = CalculateRays(light.innerSpotAngle);
        Ray[] outerRays = CalculateRays(light.innerSpotAngle + (light.spotAngle - light.innerSpotAngle) / 2);

        float range = light.range;

        //Raycast to align to a plane
        RaycastHit centreHit;
        //Inner section for hard light
        //RaycastHit[] innerHit = new RaycastHit[4];
        RaycastHit innerHit, outerHit;
        Physics.Raycast(transform.position, transform.forward, out centreHit, range);
        centrePos = centreHit.point;
        for (int i = 0; i < 4; i++)
        {
            Physics.Raycast(innerRays[i].origin, innerRays[i].direction, out innerHit, range);
            Physics.Raycast(outerRays[i].origin, outerRays[i].direction, out outerHit, range);
            innerPos[i] = innerHit.point;
            outerPos[i] = outerHit.point;
        }
    }

    private float CalculateLightValue(Vector3[] projP)
    {
        float[] ratios = new float[4];

        for (int i = 0; i < 4; i++)
        {
            Vector3 OuterLine = outerPos[i] - centrePos;
            Vector3 innerLine = innerPos[i] - centrePos;
            Vector3 projLine = projP[i] - centrePos;
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
