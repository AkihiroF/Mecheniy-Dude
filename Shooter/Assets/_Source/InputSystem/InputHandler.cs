using _Source.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source.InputSystem
{
    public class InputHandler
    {
        private PlayerMovement _movement;

        public InputHandler(PlayerMovement movement)
        {
            _movement = movement;
        }

        public void MoveAction(InputAction.CallbackContext obj)
        {
            var direction = obj.ReadValue<Vector2>();
            //Debug.Log(direction);
            //_movement.SetDirectionMoving(direction);
        }
    }
}