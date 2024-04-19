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
        private float findPlayerInterval = 10000f;

        [SerializeField]
        [Tooltip("The distance at which the enemy will stop moving towards the player.")]
        private float targetDistance = 1000000f;

        [SerializeField]
        [Tooltip("The bullet prefab to shoot.")]
        private GameObject bulletPrefab;

        [SerializeField]
        [Tooltip("The transform from which bullets will be spawned.")]
        private Transform firePoint;

        [SerializeField]
        [Tooltip("The speed of the bullet.")]
        private float bulletSpeed = 40f;

        private Transform target;
        private Coroutine findPlayerCoroutine;
        private NavMeshAgent agent;
        private RotateAgentSmoothly rotateAgent;

        protected override void Awake()
        {
            base.Awake();

            agent = GetComponent<NavMeshAgent>();
            agent.speed = StatsHandler.CurrentStats.MovementSpeed;
            agent.stoppingDistance = targetDistance;
            agent.updateUpAxis = false;

            rotateAgent = new RotateAgentSmoothly(agent, GetComponent<AgentOverride2d>(), 180f);

            HealthHandler.OnDeath.AddListener(Die);
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

            // Стрельба в направлении игрока
            Shoot();
        }

        private void Shoot()
        {
            if (Vector3.Distance(transform.position, target.position) <= targetDistance)
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Задаем направление пули в сторону игрока
        Vector2 direction = (target.position - firePoint.position).normalized;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // Добавляем начальный импульс в направлении движения врага, чтобы пули не "застывали" около врагов
        bulletRb.AddForce(agent.velocity, ForceMode2D.Impulse);
    }
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
