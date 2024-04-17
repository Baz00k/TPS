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
        [Tooltip("The distance at which the enemy will stop moving towards the player.")]
        private float targetDistance = 10f;

        private Transform target;
        private Coroutine findPlayerCoroutine;
        private NavMeshAgent agent;
        private RotateAgentSmoothly rotateAgent;

        protected override void Awake()
        {
            base.Awake();

            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = targetDistance;
            agent.updateUpAxis = false;

            rotateAgent = new RotateAgentSmoothly(agent, GetComponent<AgentOverride2d>(), 180f);

            HealthHandler.OnDeath.AddListener(Die);
            StatsHandler.OnStatsChanged.AddListener(stats => agent.speed = stats.MovementSpeed);
        }

        protected void Start()
        {
            if (target == null)
            {
                findPlayerCoroutine ??= StartCoroutine(FindPlayerCoroutine());
            }
        }

        protected void Update()
        {
            if (target == null)
            {
                findPlayerCoroutine ??= StartCoroutine(FindPlayerCoroutine());
                return;
            }

            agent.SetDestination(target.position);
            rotateAgent.UpdateAgent();
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
