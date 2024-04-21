using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Extensions;

namespace TPS.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentOverride2d))]
    public class EnemyController : BaseCharacterController
    {
        [SerializeField]
        [Tooltip("The tag of the player GameObject.")]
        private string PlayerTag = "Player";

        [SerializeField]
        [Tooltip("The interval at which the enemy will search for the player.")]
        private float findPlayerInterval = 1f;

        [SerializeField]
        [Tooltip("The distance at which the enemy will stop moving towards the player and starts attacking.")]
        private float targetDistance = 10f;

        [SerializeField]
        [Tooltip("Distance at which the enemy will start following the player.")]
        private float followDistance = 20f;

        protected Transform target;
        private Coroutine findPlayerCoroutine;
        protected NavMeshAgent agent;
        protected RotateAgentSmoothly rotateAgent;

        protected override void Awake()
        {
            base.Awake();

            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = targetDistance;
            agent.updateUpAxis = false;

            rotateAgent = new RotateAgentSmoothly(agent, GetComponent<AgentOverride2d>(), 180f);

            StatsHandler.OnStatsChanged.AddListener(stats => agent.speed = stats.MovementSpeed);
            HealthHandler.OnDeath.AddListener(Die);
        }

        protected void Start()
        {
            if (target == null)
            {
                findPlayerCoroutine ??= StartCoroutine(FindPlayerCoroutine());
            }
        }

        protected virtual void Update()
        {
            if (target == null)
            {
                findPlayerCoroutine ??= StartCoroutine(FindPlayerCoroutine());
                return;
            }

            // Stop following player if it's too far
            if (Vector3.Distance(transform.position, target.position) >= followDistance) return;

            agent.SetDestination(target.position);
            rotateAgent.UpdateAgent();

            if (Vector3.Distance(transform.position, target.position) <= targetDistance)
            {
                Attack();
            }
        }

        private void Attack()
        {
            InventoryHandler.UseActiveItem();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private IEnumerator FindPlayerCoroutine()
        {
            while (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);

                if (player != null)
                {
                    target = player.transform;
                    findPlayerCoroutine = null;
                }

                yield return new WaitForSeconds(findPlayerInterval);
            }
        }
    }
}
