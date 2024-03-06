using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float fireRate = 1f;

    [SerializeField]
    [Range(1f, 100f)]
    private float bulletSpeed = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float range = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float damage = 10f;

    private float nextFireTime = 0f;

    public void Fire()
    {
        if (Time.time > nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.SetSpeed(bulletSpeed);
            bulletController.SetRange(range);
            bulletController.SetDamage(damage);

            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
