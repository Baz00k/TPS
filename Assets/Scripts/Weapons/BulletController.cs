using UnityEngine;
using TPS.Characters;


namespace TPS.Weapons
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletController : MonoBehaviour
    {
        private float speed = 10f;
        private float range = 10f;
        private float damage = 10f;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        private void Start()
        {
            rb.velocity = transform.up * speed;
            Destroy(gameObject, range / speed);
        }

        public void SetStats(float speed, float range, float damage)
        {
            this.speed = speed;
            this.range = range;
            this.damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<CharacterHealthHandler>(out var healthHandler))
            {
                healthHandler.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
