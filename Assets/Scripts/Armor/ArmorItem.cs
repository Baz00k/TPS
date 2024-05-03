using UnityEngine;

namespace TPS.Armor
{
    public class ArmorItem
    {
        public Armor armor;
        public float currentDurability;

        public ArmorItem(Armor armorStats)
        {
            armor = ScriptableObject.CreateInstance<Armor>();
            armor.armorName = armorStats.armorName;
            armor.baseDurability = armorStats.baseDurability;
            armor.DMGResistance = armorStats.DMGResistance;
            armor.icon = armorStats.icon;

            currentDurability = armorStats.baseDurability;
        }
    }

}
