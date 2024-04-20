using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPS.Characters;

 public class ExplosiveEnemyCollision : MonoBehaviour
    {
        public int damageAmount = 20; // Количество урона

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("chuj");
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Столкновение с игроком");

                // Проверяем, принадлежит ли столкнувшийся объект к врагу с тегом "ExplosiveEnemy"
                if (gameObject.CompareTag("ExplosiveEnemy"))
                {
                    Debug.Log("Это враг ExplosiveEnemy");

                    // Получаем компонент здоровья игрока
                    CharacterHealthHandler playerHealth = collision.collider.GetComponent<CharacterHealthHandler>();

                    // Если у игрока есть компонент здоровья, то наносим урон
                    if (playerHealth != null)
                    {
                        Debug.Log("Наносим урон игроку");
                        playerHealth.Damage(damageAmount);
                    }
                    else
                    {
                        Debug.LogWarning("CharacterHealthHandler component not found on player object.");
                    }
                }
                else
                {
                    Debug.LogWarning("Это не враг ExplosiveEnemy");
                }
            }
        }
    }
