using UnityEngine;


namespace TPS.Weapons
{
    [RequireComponent(typeof(Collider2D))]
    public class BulletController : MonoBehaviour
    {
        private float speed = 10f;
        private float range = 10f;
        private float damage = 10f;

        public void SetStats(float speed, float range, float damage)
        {
            this.speed = speed;
            this.range = range;
            this.damage = damage;
        }

        private void FixedUpdate()
        {
            transform.Translate(speed * Time.fixedDeltaTime * Vector2.up);
            range -= speed * Time.fixedDeltaTime;

            if (range <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet hit " + other.name);
        }

    }
}
