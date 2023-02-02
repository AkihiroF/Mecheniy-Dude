using _Source.FireSystem.Player;
using UnityEngine.InputSystem;

namespace _Source.InputSystem
{
    public class InputHandler
    {
        private PlayerFireSystem _fireSystem;

        public InputHandler(PlayerFireSystem playerFireSystem)
        {
            _fireSystem = playerFireSystem;
        }


        public void InputFire(InputAction.CallbackContext obj)
        {
            _fireSystem.Fire();
        }
    }
}