using UnityEngine;
using UnityEngine.UI;
using TPS.Characters;

namespace TPS.UI
{
    public class ArmorBar : MonoBehaviour
    {
        [SerializeField] private GameObject entityToWatch;
        [SerializeField] private Slider slider;
        private const float MaxSliderValue = 100f;

        private CharacterHealthHandler healthHandler;

        private void Awake()
        {
            if (entityToWatch == null)
            {
                Debug.LogError("ArmorBar: entityToWatch is not set!");
            }

            var healthHandlerExists = entityToWatch.TryGetComponent<CharacterHealthHandler>(out healthHandler);

            if (!healthHandlerExists)
            {
                Debug.LogError("ArmorBar: entityToWatch does not have a CharacterHealthHandler!");
                return;
            }

            healthHandler.OnArmorDurabilityChange.AddListener(UpdateArmorBar);
        }

        private void OnDestroy()
        {
            healthHandler.OnArmorDurabilityChange.RemoveListener(UpdateArmorBar);
        }

        private void UpdateArmorBar(ArmorItem armorItem)
        {
            ArmorItem lowestArmor = healthHandler.GetLowestIndexArmor();

            if (lowestArmor == null)
            {
                DestroyArmor();
                return;
            }

            slider.maxValue = MaxSliderValue; // Ustaw max wartość slidera na 100
            slider.value = lowestArmor.durability;  // Aktualizujesz wartość dla durability najniższego armoru

            if (armorItem.durability <= 0)  // Sprawdzasz durability aktualnego armoru
            {
                DestroyArmor();
            }
        }

        private void DestroyArmor()
        {
            ArmorItem lowestArmor = healthHandler.GetLowestIndexArmor();

            if (lowestArmor != null)
            {
                // Usuń najniższy obiekt ArmorItem z ArmorManager
                ArmorManager.Instance.RemoveArmor(lowestArmor);
            }
        }
    }
}
