using UnityEngine;

namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(CharacterInventoryHandler))]
    public abstract class BaseCharacterController : MonoBehaviour
    {
        protected CharacterStatsHandler StatsHandler { get; private set; }
        protected CharacterInventoryHandler InventoryHandler { get; private set; }

        protected virtual void Awake()
        {
            StatsHandler = GetComponent<CharacterStatsHandler>();
            InventoryHandler = GetComponent<CharacterInventoryHandler>();
        }
    }
}
