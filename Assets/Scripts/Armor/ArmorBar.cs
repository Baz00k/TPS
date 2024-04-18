using UnityEngine;
using UnityEngine.UI;
using TPS.Characters;

namespace TPS.UI
{
    public class ArmorBar : MonoBehaviour
    {
        [SerializeField] private GameObject entityToWatch;
        [SerializeField] private Slider slider;
        [SerializeField] private Image armorIcon;

        private CharacterHealthHandler healthHandler;

        private void Awake()
        {
            if (entityToWatch == null)
            {
                Debug.LogError("ArmorBar: entityToWatch is not set!");
                return;
            }

            if (!entityToWatch.TryGetComponent(out healthHandler))
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
            slider.value = armorItem.durability;  // Aktualizujesz wartość dla aktualnie podnoszonego armoru

            if (armorItem.durability <= 0)
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

                // Aktualizuj ikonę armoru na nową wartość
                ArmorItem newLowestArmor = healthHandler.GetLowestIndexArmor();
                if (newLowestArmor != null && armorIcon != null)
                {
                    armorIcon.sprite = newLowestArmor.icon;
                }
            }
        }
    }
}



