using UnityEngine;

namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(CharacterInventoryHandler))]
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class BaseCharacterController : MonoBehaviour
    {
        protected CharacterStatsHandler StatsHandler { get; private set; }
        protected CharacterInventoryHandler InventoryHandler { get; private set; }
        protected CharacterMovement Movement { get; private set; }

        protected virtual void Awake()
        {
            StatsHandler = GetComponent<CharacterStatsHandler>();
            InventoryHandler = GetComponent<CharacterInventoryHandler>();
            Movement = GetComponent<CharacterMovement>();
        }
    }
}
