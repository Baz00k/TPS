using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TPS.Characters
{
    public class DamageOnTouch : MonoBehaviour
    {
        public int damageAmount = 10;
        private CharacterStatsHandler characterStatsHandler;

        void Start()
        {

            characterStatsHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                characterStatsHandler.TakeDamage(damageAmount);
            }
        }
    }
}
