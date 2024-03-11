using UnityEngine;
using UnityEngine.InputSystem;

namespace TPS.Characters
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : BaseCharacterController
    {
        private bool isUsingItem;

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
