using UnityEngine;

namespace TPS.Characters
{
    public class CharacterStatsHandler : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The default stats of the character")]
        private CharacterStats baseStats;

        public int maxHealth = 100;
	    public int currentHealth;
	    public HealthBar healthBar;

        void Start()
        {
		    currentHealth = maxHealth;
		    healthBar.SetMaxHealth(maxHealth);
        }
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

        public void TakeDamage(int damage)
	    {
		    currentHealth -= damage;

		    healthBar.SetHealth(currentHealth);
	    }
    }
}
