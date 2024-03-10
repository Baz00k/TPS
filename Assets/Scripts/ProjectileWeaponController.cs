using UnityEngine;

public class ProjectileWeaponController : BaseInventoryItem
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float fireRate = 1f;

    [SerializeField]
    [Range(1f, 100f)]
    private float projectileSpeed = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float range = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float damage = 10f;

    private float fireRateTimer = 0f;

    public override void Use(bool _) => Fire();

    private void Fire()
    {
        if (Time.time > fireRateTimer)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.SetStats(projectileSpeed, range, damage);

            fireRateTimer = Time.time + 1f / fireRate;
        }
    }
}
