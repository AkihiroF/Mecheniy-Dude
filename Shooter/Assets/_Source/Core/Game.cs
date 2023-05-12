using System;
using _Source.HealthSystem;
using _Source.InputSystem;
using DG.Tweening;
using UnityEngine;

namespace _Source.Core
{
    public class Game
    {
        public static event Action OnRestart;
        public static event Action OnPaused;
        public Game(Input input, InputHandler inputHandler)
        {
            _input = input;
            _inputHandler = inputHandler;
            PlayerHealth.OnDead += PausedGame;
            _inputHandler.SetGame(this);
        }

        private Input _input;
        private InputHandler _inputHandler;

        private void Bind()
        {
            var input = _input.Player;
            input.Fire.performed += _inputHandler.InputFire;
            input.Reload.performed += _inputHandler.InputReload;
            input.Healing.performed += _inputHandler.InputHealing;
            input.Interactive.performed += _inputHandler.InputInteractive;
            input.ChooseKnife.performed += _inputHandler.InputChooseKnife;
            input.ChoosePistol.performed += _inputHandler.InputChoosePistol;
            input.ChooseShortGun.performed += _inputHandler.InputChooseShortGun;
            input.ChooseRifle.performed += _inputHandler.InputChooseRifle;

            _input.Interface.Paused.performed += _inputHandler.InputPaused;
        }

        private void UnBind()
        {
            var input = _input.Player;
            input.Fire.performed -= _inputHandler.InputFire;
            input.Reload.performed -= _inputHandler.InputReload;
            input.Healing.performed -= _inputHandler.InputHealing;
            
            _input.Interface.Paused.performed -= _inputHandler.InputPaused;
        }
        private void EnablePlayerInput() 
            => _input.Player.Enable();

        private void DisablePlayerInput()
            => _input.Player.Disable();

        public void StartGame()
        {
            EnablePlayerInput();
            Bind();
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            if (OnRestart != null) OnRestart.Invoke();
            DOTween.Clear();
        }

        public void PausedGame()
        {
            DisablePlayerInput();
            UnBind();
            Time.timeScale = 0;
            if (OnPaused != null) OnPaused.Invoke();
        }
    }
}