using UnityEngine;
using UnityEngine.UI;
using TPS.Characters;

namespace TPS.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject entityToWatch;
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;

        private CharacterHealthHandler healthHandler;
        private CharacterStatsHandler statsHandler;

        private void Awake()
        {
            if (entityToWatch == null)
            {
                Debug.LogError("HealthBar: entityToWatch is not set!");
            }

            var healthHandlerExists = entityToWatch.TryGetComponent<CharacterHealthHandler>(out healthHandler);

            if (!healthHandlerExists)
            {
                Debug.LogError("HealthBar: entityToWatch does not have a CharacterHealthHandler!");
                return;
            }

            healthHandler.OnHealthChange.AddListener(SetHealth);

            statsHandler = entityToWatch.GetComponent<CharacterStatsHandler>();
            statsHandler.OnStatsChanged.AddListener(SetMaxHealth);
        }

        public void SetMaxHealth(CharacterStats characterStats)
        {
            slider.maxValue = characterStats.MaxHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
