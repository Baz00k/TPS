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
        OnArmorDurabilityChange.Invoke(CurrentArmor);
    }

    public void AddArmor(ArmorItem armor)
    {
        armors.Add(armor);
        armors.Sort((a, b) => b.armor.DMGResistance.CompareTo(a.armor.DMGResistance));

        OnArmorChange.Invoke(CurrentArmor);
        OnArmorDurabilityChange.Invoke(CurrentArmor);
    }

    public void DamageArmor(float amount)
    {
        if (CurrentArmor == null)
        {
            return;
        }

        CurrentArmor.currentDurability -= amount;
        OnArmorDurabilityChange.Invoke(CurrentArmor);

        if (CurrentArmor.currentDurability <= 0)
        {
            RemoveArmor(CurrentArmor);
        }
    }

    public void RemoveArmor(ArmorItem armor)
    {
        armors.Remove(armor);
        OnArmorChange.Invoke(CurrentArmor);
        OnArmorDurabilityChange.Invoke(CurrentArmor);
    }
}
