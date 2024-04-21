using UnityEngine;

namespace TPS.Armor
{
    public class ArmorPickup : MonoBehaviour
    {
        public Armor armor;

        public ArmorItem GetArmorItem()
        {
            return new ArmorItem(armor);
        }
    }
}
