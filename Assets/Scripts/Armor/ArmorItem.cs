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

        public ArmorItem(Armor armor)
        {
            this.armor = armor;
            this.currentDurability = armor.baseDurability;
        }
    }

}
