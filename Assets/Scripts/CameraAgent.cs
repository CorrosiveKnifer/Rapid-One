using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// <Author> Michael Jordan </Author>
/// </summary>
public class CameraAgent : MonoBehaviour
{
    //Target Transforms
    public Transform childLocation;
    public Transform parentLocation;
    public MeshRenderer parentMesh;

    //Navigation
    private NavMeshAgent agent;

    public int state = 0; //+1 = adult -1 = child
    public bool isShifting = false;

    //Current shifting target transform
    private Transform targetLocation;
    //Save of the transform before shifting
    private Transform shiftingTransform;

    private MeshRenderer parentRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetLocation = parentLocation;
        state = 1;
        shiftingTransform = transform;
    }

    void Update()
    {
        if(isShifting)
        {
            //Transition to target location using the NavMesh
            agent.destination = targetLocation.position;

            //Current Distance to target location
            float dist = Vector3.Distance(transform.position, targetLocation.position);

            //Calculate current scale
            transform.localScale =  LerpToTarget(targetLocation.localScale, shiftingTransform.localScale, dist, 2.5f);

            if(dist < 2.5f)
            {
                parentMesh.enabled = false;
            }
        }
        else
        {
            //Force the agent to the current location
            transform.position = (state == 1) ? parentLocation.position: childLocation.position;
            agent.height = (state == -1) ? 0.05f : 1.0f;
        }

        //While shifting and the position is at the target position
        float distance = Vector3.Distance(transform.position, targetLocation.position);
        if(isShifting && distance < 1.1f)
        {
            //Disable shifting
            isShifting = false;
            parentMesh.enabled = true;
            //Set state
            state = (targetLocation.position.Equals(childLocation.position)) ? -1 : 1;
        }
    }

    /// <summary>
    /// External Function to start the shifting process
    /// </summary>
    public void Shift()
    {
        isShifting = true;
        shiftingTransform = transform;
        if (IsPositionEquals(transform.position, childLocation.position))
        {
            targetLocation = childLocation;
        }
        else
        {
            targetLocation = parentLocation;
        }
    }
    
    /// <summary>
    /// Checks the vector3 position if they are equal, removing floating point errors.
    /// </summary>
    /// <param name="pos1">Position One</param>
    /// <param name="pos2">Position Two</param>
    /// <param name="threshold">Ajustment for floating point issues</param>
    /// <returns>If two positions are equal</returns>
    private bool IsPositionEquals(Vector3 pos1, Vector3 pos2, float threshold = 0.05f)
    {
        return (Vector3.SqrMagnitude(pos1 - pos2) < threshold);
    }

    /// <summary>
    /// Return a lerp value based on the distance away from the target.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <param name="distance"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    private Vector3 LerpToTarget(Vector3 current, Vector3 target, float distance, float threshold = 1.5f)
    {
        float distRatio = Mathf.Clamp(distance / threshold, 0.0f, 1.0f);

        Vector3 retValue = Vector3.Lerp(current, target, distRatio);

        return retValue;
    }
}
