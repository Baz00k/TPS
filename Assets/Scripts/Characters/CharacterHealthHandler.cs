using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;


namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    public class CharacterHealthHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> onHealthChange;
        [SerializeField] private UnityEvent onHeal;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDeath;

        private CharacterStatsHandler statsHandler;

        public UnityEvent OnHeal => onHeal;
        public UnityEvent OnDamage => onDamage;
        public UnityEvent OnDeath => onDeath;
        public UnityEvent<float> OnHealthChange => onHealthChange;
        public float MaxHealth => statsHandler.CurrentStats.MaxHealth;

        public float CurrentHealth { get; private set; }

        private void Awake()
        {
            statsHandler = GetComponent<CharacterStatsHandler>();
            ArmorManager.Instance.OnArmorChanged += UpdateDamageResistance;
        }

        private void Start()
        {
            CurrentHealth = statsHandler.CurrentStats.MaxHealth;
            onHealthChange.Invoke(CurrentHealth);
        }

        private void OnDestroy()
        {
            ArmorManager.Instance.OnArmorChanged -= UpdateDamageResistance;
        }

        private void UpdateDamageResistance(ArmorItem newArmor)
        {
            // Aktualizuj obrażenia w zależności od nowego armoru
            // Tymczasowo pomiń, ponieważ będziemy aktualizować to przy każdym obrażeniu
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
            // Pobierz najwyższy index z listy ArmorItem
            ArmorItem highestArmor = GetLowestIndexArmor();

            // Oblicz obrażenia z uwzględnieniem DMGResistance
            float damage = amount * (1 - highestArmor.DMGResistance);

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, statsHandler.CurrentStats.MaxHealth);
            onDamage.Invoke();
            onHealthChange.Invoke(CurrentHealth);

            UnityEngine.Debug.Log($"Otrzymane obrażenia: {damage}, Aktualny stan zdrowia: {CurrentHealth}");

            if (CurrentHealth == 0)
            {
                onDeath.Invoke();
            }
        }

        private ArmorItem GetLowestIndexArmor()
{
    if (ArmorManager.Instance.Armors.Count == 0)
    {
        return null;
    }

    int lowestIndex = 0;
    ArmorItem lowestArmor = ArmorManager.Instance.Armors[lowestIndex];

    for (int i = 1; i < ArmorManager.Instance.Armors.Count; i++)
    {
        if (ArmorManager.Instance.Armors[i].id < lowestArmor.id)
        {
            lowestArmor = ArmorManager.Instance.Armors[i];
            lowestIndex = i;
        }
    }

    return lowestArmor;
}


    }
}

