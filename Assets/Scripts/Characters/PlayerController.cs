using UnityEngine;
using UnityEngine.InputSystem;

namespace TPS.Characters
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : BaseCharacterController
    {
        private bool isUsingItem;
        private CharacterMovement Movement { get; set; }

        protected override void Awake()
        {
            base.Awake();

            Movement = GetComponent<CharacterMovement>();
        }

        public void OnMove(InputValue value)
        {
            Movement.Move(value.Get<Vector2>());
        }

        public void OnInventoryChange(InputValue value)
        {
            int change = (int)value.Get<float>();
            if (change == 0) return;

            InventoryHandler.ChangeActiveItem(change);
        }

        public void OnUseItem(InputValue value)
        {
            isUsingItem = value.isPressed;
        }

        public void OnPickupItem(InputValue value)
        {
            if (value.isPressed)
            {
                InventoryHandler.PickupItem();
            }
        }

        private void RotateTowardsMouse()
        {
            // Rotate the character towards the mouse position
            // TODO: Use InputSystem for this
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Vector2 direction = new(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            transform.up = direction;
        }

        private void Update()
        {
            RotateTowardsMouse();

            if (isUsingItem)
            {
                InventoryHandler.UseActiveItem();
            }
        }
    }
}
