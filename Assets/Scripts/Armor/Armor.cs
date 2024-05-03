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
}
