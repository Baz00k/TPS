using UnityEngine;

namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(CharacterInventoryHandler))]
    [RequireComponent(typeof(CharacterHealthHandler))]
    public abstract class BaseCharacterController : MonoBehaviour
    {
        protected CharacterStatsHandler StatsHandler { get; private set; }
        protected CharacterInventoryHandler InventoryHandler { get; private set; }
        protected CharacterHealthHandler HealthHandler { get; private set; }

        protected virtual void Awake()
        {
            StatsHandler = GetComponent<CharacterStatsHandler>();
            InventoryHandler = GetComponent<CharacterInventoryHandler>();
            HealthHandler = GetComponent<CharacterHealthHandler>();
        }
    }
}
