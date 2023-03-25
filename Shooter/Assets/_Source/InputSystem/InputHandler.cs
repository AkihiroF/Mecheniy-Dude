using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Interactable;
using UnityEngine.InputSystem;

namespace _Source.InputSystem
{
    public class InputHandler
    {
        private readonly PlayerFireSystem _fireSystem;
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerInteractiveComponent _playerInteractive;

        public InputHandler(PlayerFireSystem playerFireSystem, PlayerHealth playerHealth, PlayerInteractiveComponent playerInteractive)
        {
            _fireSystem = playerFireSystem;
            _playerHealth = playerHealth;
            _playerInteractive = playerInteractive;
        }


        public void InputFire(InputAction.CallbackContext obj)
        {
            _fireSystem.Fire();
        }

        public void InputReload(InputAction.CallbackContext obj)
        {
            _fireSystem.ReloadWeapon();
        }

        public void InputHealing(InputAction.CallbackContext obj)
        {
            _playerHealth.UseKit();
        }

        public void InputInteractive(InputAction.CallbackContext obj)
        {
            _playerInteractive.GetItem();
        }
    }
}