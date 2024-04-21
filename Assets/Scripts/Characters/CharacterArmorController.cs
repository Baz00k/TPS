using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TPS.Armor;

public class CharacterArmorController : MonoBehaviour
{
    private readonly List<ArmorItem> armors = new();
    [SerializeField] private UnityEvent<ArmorItem> onArmorChange;
    public UnityEvent<ArmorItem> OnArmorChange => onArmorChange;
    [SerializeField] private UnityEvent<ArmorItem> onArmorDurabilityChange;
    public UnityEvent<ArmorItem> OnArmorDurabilityChange => onArmorDurabilityChange;

    public ArmorItem CurrentArmor => armors.Count > 0 ? armors[0] : null;

    private void Start()
    {
        OnArmorChange.Invoke(CurrentArmor);
    }

    public void AddArmor(ArmorItem armor)
    {
        armors.Add(armor);
        armors.Sort((a, b) => b.armor.DMGResistance.CompareTo(a.armor.DMGResistance));

        OnArmorChange.Invoke(armor);
    }

    public void RemoveArmor(ArmorItem armor)
    {
        armors.Remove(armor);
        OnArmorChange.Invoke(armors.Count > 0 ? armors[0] : null);
    }

    public void DamageArmor(float amount)
    {
        if (armors.Count == 0)
        {
            return;
        }

        armors[0].currentDurability -= amount;
        OnArmorDurabilityChange.Invoke(armors[0]);

        if (armors[0].currentDurability <= 0)
        {
            RemoveArmor(armors[0]);
        }
    }


}
