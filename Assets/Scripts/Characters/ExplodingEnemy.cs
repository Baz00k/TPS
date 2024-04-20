using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPS.Characters;

public class ExplosiveEnemy : MonoBehaviour
{
    public GameObject explosionEffect; // Префаб эффекта взрыва
    public float speed = 3f; // Скорость движения врага
    public int damage = 10; // Урон, наносимый игроку
    public int health = 250; // Здоровье врага

    private bool facingRight = true; // Направление взгляда врага

    void Update()
    {
        // Движение в направлении игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);

            // Поворот взрывающегося врага в направлении движения
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // Проверяем, прикоснулась ли пуля игрока к врагу
        {
            Debug.Log("Enemy hit by bullet."); // Добавим отладочное сообщение для проверки попадания пули
            Damage(15); // Наносим урон врагу
        }
    }

    void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Enemy died."); // Добавим отладочное сообщение для проверки смерти врага

        if (explosionEffect != null)
        {
            Debug.Log("Explosion!"); // Добавим отладочное сообщение для проверки взрыва
            Instantiate(explosionEffect, transform.position, Quaternion.identity); // Создаем эффект взрыва в позиции врага
            Destroy(gameObject); // Уничтожаем врага
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        Debug.Log("Enemy flipped."); // Добавим отладочное сообщение для проверки поворота врага
    }
}
