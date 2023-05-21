using _Source.Core;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source.InputSystem
{
    public class InputHandler
    {
        private readonly PlayerFireSystem _fireSystem;
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerInteractiveComponent _playerInteractive;
        private  Game _game;

        public InputHandler(PlayerFireSystem playerFireSystem,
            PlayerHealth playerHealth,
            PlayerInteractiveComponent playerInteractive)
        {
            _fireSystem = playerFireSystem;
            _playerHealth = playerHealth;
            _playerInteractive = playerInteractive;
        }

        public void SetGame(Game game) 
            => _game = game;

        public void InputFire(InputAction.CallbackContext obj) 
            => _fireSystem.Fire();

        public void InputReload(InputAction.CallbackContext obj) 
            => _fireSystem.ReloadWeapon();

        public void InputHealing(InputAction.CallbackContext obj) 
            => _playerHealth.UseKit();

        public void InputInteractive(InputAction.CallbackContext obj)
        {
            _playerInteractive.GetItem();
            _playerHealth.UpdateStateUI();
            _fireSystem.PrintAmmo();
        }

        public void InputPaused(InputAction.CallbackContext obj) 
            => _game.PausedGame();

        public void InputChooseKnife(InputAction.CallbackContext obj)
        {
            _fireSystem.SwitchWeapon(WeaponsTypes.Knife);
        }

        public void InputChoosePistol(InputAction.CallbackContext obj)
        {
            _fireSystem.SwitchWeapon(WeaponsTypes.Pistol);
        }

        public void InputChooseShortGun(InputAction.CallbackContext obj)
        {
            _fireSystem.SwitchWeapon(WeaponsTypes.ShortGun);
        }

        public void InputChooseRifle(InputAction.CallbackContext obj)
        {
            _fireSystem.SwitchWeapon(WeaponsTypes.Rifle);
        }

        public void TestDelta(InputAction.CallbackContext obj)
        {
            Debug.Log(obj);
        }
    }
}