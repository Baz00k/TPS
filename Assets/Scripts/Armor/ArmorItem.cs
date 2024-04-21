using UnityEngine;

namespace TPS.Armor
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
    public class Armor : ScriptableObject
    {
        public string armorName;
        public int baseDurability;
        public float DMGResistance;
        public Sprite icon;
    }

    public class ArmorItem
    {
        public Armor armor;
        public float currentDurability;

        public ArmorItem(Armor armorStats)
        {
            armor = armorStats;
            currentDurability = armorStats.baseDurability;
        }
    }

}
