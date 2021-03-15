using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// <Author> Michael Jordan </Author>
/// </summary>
public class CameraAgent : MonoBehaviour
{
    public enum AgentState { FOLLOW_ADULT, FOLLOW_CHILD, SHIFTTING};
    public AgentState currentState;

    //Target Transforms
    public Transform childLocation;
    public Transform parentLocation;
    public MeshRenderer parentMesh;

    //Navigation
    private NavMeshAgent agent;

    //Current shifting target transform
    private Transform targetLocation;
    //Save of the transform before shifting
    private Transform shiftingTransform;

    private MeshRenderer parentRenderer;

    void Start()
    {
        currentState = AgentState.FOLLOW_ADULT;
        agent = GetComponent<NavMeshAgent>();
        targetLocation = parentLocation;
        shiftingTransform = transform;
    }

    void Update()
    {
        switch (currentState)
        {
            default:
            case AgentState.FOLLOW_ADULT:
                {
                    //Teleport the agent to parent's position.
                    transform.position = parentLocation.position;
                    agent.height = 2.0f;
                    break;
                }
            case AgentState.FOLLOW_CHILD:
                {
                    //Teleport the agent to child's position.
                    transform.position = childLocation.position;
                    agent.height = 1.0f;
                    break;
                }
            case AgentState.SHIFTTING:
                {
                    //Activate the navigation
                    agent.destination = targetLocation.position;

                    float dist = Vector3.Distance(transform.position, targetLocation.position);
                    transform.localScale = LerpToTarget(targetLocation.localScale, shiftingTransform.localScale, dist, 2.5f);
                    parentMesh.enabled = (dist > 1.5f);

                    //Check if movement is done.
                    Vector2 loc = new Vector2(transform.position.x, transform.position.z);
                    Vector2 target = new Vector2(targetLocation.position.x, targetLocation.position.z);

                    if(Vector2.Distance(loc, target) < 0.5f)
                    {
                        currentState = (targetLocation == parentLocation) ? AgentState.FOLLOW_ADULT : AgentState.FOLLOW_CHILD;
                        parentMesh.enabled = true;
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// External Function to start the shifting process
    /// </summary>
    public void Shift()
    {
        //isShifting = true;
        shiftingTransform = transform;

        switch (currentState)
        {
            case AgentState.FOLLOW_ADULT:
                {
                    targetLocation = childLocation;
                    break;
                }
            case AgentState.FOLLOW_CHILD:
                {
                    targetLocation = parentLocation;
                    break;
                }
            default:
            case AgentState.SHIFTTING:
                break;
        }

        currentState = AgentState.SHIFTTING;
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
