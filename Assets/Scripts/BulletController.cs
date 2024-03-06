using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed;
    private float range;
    private float damage;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetDamage(float damage)
    {
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
