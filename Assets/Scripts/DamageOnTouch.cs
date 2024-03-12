using UnityEngine;
using TPS.Characters;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private float damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterHealthHandler>(out var healthHandler))
        {
            healthHandler.Damage(damage);
        }
    }
}
