using UnityEngine;

namespace TPS.Armor
{
    public class ArmorPickup : MonoBehaviour
    {

        [SerializeField]
        private Armor armor;

        public ArmorItem GetArmorItem()
        {
            return new ArmorItem(armor);
        }
    }
}
