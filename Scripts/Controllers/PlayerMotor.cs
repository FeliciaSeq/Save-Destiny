using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {

        Debug.Log("Moving to point: " + point);

        // Disable auto braking before setting the destination
        ToggleAutoBraking(false);

        agent.SetDestination(point);

        // Enable auto braking after setting the destination
        StartCoroutine(EnableAutoBrakingAfterDelay());
    }



    public void FollowTarget(Interactable newTarget)
    {
        ToggleAutoBraking(false);

        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;

        target = newTarget.transform;

        // Enable auto braking after setting the stopping distance
        StartCoroutine(EnableAutoBrakingAfterDelay());
    }


    

   
    public void StopFollowingTarget()
    {
        ToggleAutoBraking(false);

        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;

        // Enable auto braking after setting the stopping distance
        StartCoroutine(EnableAutoBrakingAfterDelay());
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ToggleAutoBraking(bool enable)
    {
        agent.autoBraking = enable;
    }

    IEnumerator EnableAutoBrakingAfterDelay()
    {
        // Wait for a short delay before enabling auto braking
        yield return new WaitForSeconds(0.1f);

        ToggleAutoBraking(true);
    }
}
