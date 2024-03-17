using UnityEngine;
using UnityEngine.Events;

namespace TPS.Characters
{
    public class CharacterStatsHandler : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The default stats of the character")]
        private CharacterStats baseStats;
        public CharacterStats CurrentStats { get; private set; }

        [Header("Events")]
        [SerializeField] private UnityEvent<CharacterStats> onStatsChanged;
        public UnityEvent<CharacterStats> OnStatsChanged => onStatsChanged;

        private void Awake()
        {
            ApplyStats(baseStats);
        }

        public void ApplyStats(CharacterStats newStats)
        {
            CurrentStats = newStats;
            LimitStats();
            onStatsChanged.Invoke(CurrentStats);
        }

        public void ResetStats()
        {
            CurrentStats = baseStats;
            onStatsChanged.Invoke(CurrentStats);
        }

        private void LimitStats()
        {
            CurrentStats.MaxHealth = Mathf.Clamp(CurrentStats.MaxHealth, 1, CurrentStats.MaxHealth);
            CurrentStats.MovementSpeed = Mathf.Max(0, CurrentStats.MovementSpeed);
        }
    }
}
