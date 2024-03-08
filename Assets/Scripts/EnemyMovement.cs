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

            // Dodaj warunek sprawdzający, czy agent nadal porusza się do celu
            if (!agent.pathPending && agent.remainingDistance > 0.1f)
            {
                RotateTowardsMovementDirection();
            }
        }
        else
        {
            Debug.LogError("Player not found.");
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
        Vector3 lookDirection = (agent.steeringTarget - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Dodaj poniższą linijkę, aby natychmiastowo zastosować obroty
        agent.updateRotation = false;
        agent.updateRotation = true;
    }
}
