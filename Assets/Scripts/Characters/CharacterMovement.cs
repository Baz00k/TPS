using UnityEngine;

namespace TPS.Characters
{
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterStatsHandler statsHandler;
        private Rigidbody2D rb;
        private Vector2 moveDirection = Vector2.zero;

        private void Awake()
        {
            statsHandler = GetComponent<CharacterStatsHandler>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDirection * statsHandler.CurrentStats.MovementSpeed;
        }
    }
}
