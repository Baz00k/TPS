using UnityEngine;

namespace TPS.Characters
{
    public class CharacterStatsHandler : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The default stats of the character")]
        private CharacterStats baseStats;

        public CharacterStats CurrentStats { get; private set; }

        private void Awake()
        {
            CurrentStats = baseStats;
        }

        public void ApplyStats(CharacterStats newStats)
        {
            CurrentStats = newStats;
        }

        public void ResetStats()
        {
            CurrentStats = baseStats;
        }
    }
}
