using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TPS.UI;


namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(CharacterArmorController))]
    public class CharacterHealthHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> onHealthChange;
        [SerializeField] private UnityEvent onHeal;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDeath;

        private CharacterStatsHandler statsHandler;
        private CharacterArmorController armorController;

        public UnityEvent OnHeal => onHeal;
        public UnityEvent OnDamage => onDamage;
        public UnityEvent OnDeath => onDeath;
        public UnityEvent<float> OnHealthChange => onHealthChange;
        public float MaxHealth => statsHandler.CurrentStats.MaxHealth;

        public float CurrentHealth { get; private set; }

        private void Awake()
        {
            statsHandler = GetComponent<CharacterStatsHandler>();
            armorController = GetComponent<CharacterArmorController>();
        }

        private void Start()
        {
            CurrentHealth = statsHandler.CurrentStats.MaxHealth;
            onHealthChange.Invoke(CurrentHealth);
        }

        public void ChangeHealth(float amount)
        {
            switch (amount)
            {
                case float n when n > 0:
                    Heal(amount);
                    break;
                case float n when n < 0:
                    Damage(amount);
                    break;
            }
        }

        public void Heal(float amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, statsHandler.CurrentStats.MaxHealth);
            onHeal.Invoke();
            onHealthChange.Invoke(CurrentHealth);
        }

        public void Damage(float amount)
        {
            var currentArmor = armorController.CurrentArmor;
            var damageAmount = amount;

            if (currentArmor != null)
            {
                armorController.DamageArmor(amount);
                damageAmount = amount * (1 - currentArmor.armor.DMGResistance);
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth - damageAmount, 0, statsHandler.CurrentStats.MaxHealth);
            onDamage.Invoke();
            onHealthChange.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                onDeath.Invoke();
            }
        }
    }
}
