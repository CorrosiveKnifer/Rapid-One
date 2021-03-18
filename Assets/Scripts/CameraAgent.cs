using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Michael Jordan
/// </summary>
public class CameraAgent : MonoBehaviour
{
    public enum AgentState { FOLLOW_ADULT, FOLLOW_CHILD, SHIFTTING};
    public AgentState currentState;

    //Target Transforms
    public Transform childLocation;
    public Transform parentLocation;
    public MeshRenderer parentMesh;
    public LevelLoader loader;

    //Navigation
    private NavMeshAgent agent;

    //Current shifting target transform
    private Transform targetLocation;
    //Save of the transform before shifting
    private Transform shiftingTransform;

    private MeshRenderer parentRenderer;

    private bool enumeratorflag = false;
    private float remainingDist = -1f;
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
                    parentMesh.enabled = (dist > 1.5f);

                    //Check if movement is done.
                    Vector2 loc = new Vector2(transform.position.x, transform.position.z);
                    Vector2 target = new Vector2(targetLocation.position.x, targetLocation.position.z);

                    if(hasNavMeshGlitched())
                    {
                        if(!enumeratorflag)
                        {
                            enumeratorflag = true;
                            StartCoroutine(Blink());
                        }
                    }

                    remainingDist = agent.remainingDistance;

                    if (Vector2.Distance(loc, target) < 1.5f)
                    {
                        if(!enumeratorflag)
                        {
                            enumeratorflag = true;
                            StartCoroutine(SizeUp(2.6f));
                        }
                    }
                    break;
                }
        }
    }

    private bool hasNavMeshGlitched()
    {
        bool agentBool = agent.velocity.x == 0 && agent.velocity.z == 0 && remainingDist == agent.remainingDistance;
        return agentBool && remainingDist != -1f && !enumeratorflag;
    }

    IEnumerator Blink()
    {
        loader.GetComponentInChildren<Animator>().SetTrigger("Start");
        loader.transitionTime = 0.25f;
        loader.GetComponentInChildren<Animator>().speed = 4;

        yield return new WaitForSeconds(0.25f);

        currentState = AgentState.FOLLOW_ADULT;
        parentMesh.enabled = true;

        loader.GetComponentInChildren<Animator>().SetTrigger("Blink");
        yield return new WaitForSeconds(0.25f);

        loader.transitionTime = 1.0f;
        loader.GetComponentInChildren<Animator>().speed = 1;

        enumeratorflag = false;
        yield return null;
    }

    IEnumerator SizeUp(float target)
    {
        Vector3 startScale = transform.localScale;

        float t = 0.0f;
        float dt = 0.05f;
        float dt2 = 0.01f;

        while(transform.localScale.y < target - 0.05f)
        {
            transform.localScale = new Vector3(startScale.x, Mathf.Lerp(transform.localScale.y, target, t), startScale.z);

            t += Time.deltaTime * dt;
            dt += Time.deltaTime * dt2;

            yield return new WaitForEndOfFrame();
        }
        currentState = AgentState.FOLLOW_ADULT;
        parentMesh.enabled = true;

        transform.localScale = startScale;
        enumeratorflag = false;
        yield return null;
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
        remainingDist = -1f;
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
}
