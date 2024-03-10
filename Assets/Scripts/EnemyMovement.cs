using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        FindPlayer();

        if (target != null)
        {
            agent.SetDestination(target.transform.position);

            // Check if the agent is moving and rotate it towards the movement direction
            if (!agent.pathPending && agent.remainingDistance > 0.1f)
            {
                RotateTowardsMovementDirection();
            }
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player;
        }
    }

    void RotateTowardsMovementDirection()
    {
        Vector3 direction = (agent.steeringTarget - transform.position).normalized;
        direction.z = 0f;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);

        agent.updateRotation = true;
    }
}
