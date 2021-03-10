using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraAgent : MonoBehaviour
{
    public Transform childLocation;
    public Transform parentLocation;

    private NavMeshAgent agent;

    public int state = 0;
    public bool isShifting = false;
    private Transform targetLocation;
    private Transform shiftingTransform;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetLocation = parentLocation;
        state = 1;
        shiftingTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShifting)
        {
            agent.destination = targetLocation.position;
            float dist = Vector3.Distance(transform.position, targetLocation.position);
            transform.localScale =  LerpToTarget(targetLocation.localScale, shiftingTransform.localScale, dist, 2.5f);
        }
        else
        {
            transform.position = targetLocation.position;
        }

        if(isShifting && IsPositionEquals(transform.position, targetLocation.position))
        {
            isShifting = false;
            state = (targetLocation.position.Equals(childLocation.position)) ? -1 : 1;
        }
    }

    public void Shift()
    {
        isShifting = true;
        shiftingTransform = transform;
        if (IsPositionEquals(transform.position, childLocation.position))
        {
            targetLocation = parentLocation;
        }
        else
        {
            targetLocation = childLocation;
        }
    }

    private bool IsPositionEquals(Vector3 pos1, Vector3 pos2, float threshold = 0.05f)
    {
        return (Vector3.SqrMagnitude(pos1 - pos2) < threshold);
    }

    private Vector3 LerpToTarget(Vector3 current, Vector3 target, float distance, float threshold = 1.5f)
    {
        float distRatio = Mathf.Clamp(distance / threshold, 0.0f, 1.0f);

        Vector3 retValue = Vector3.Lerp(current, target, distRatio);

        return retValue;
    }
}
