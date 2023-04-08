using System;
using _Source.HealthSystem;
using _Source.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Core
{
    public class Game
    {
        public static event Action OnRestart; 
        public Game(Input input, InputHandler inputHandler)
        {
            _input = input;
            _inputHandler = inputHandler;
            Bind();
            PlayerHealth.OnDead += PausedGame;
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
        }

        private void UnBind()
        {
            var input = _input.Player;
            input.Fire.performed -= _inputHandler.InputFire;
            input.Reload.performed -= _inputHandler.InputReload;
            input.Healing.performed -= _inputHandler.InputHealing;
        }
        private void EnableInput() 
            => _input.Player.Enable();

        private void DisableInput()
            => _input.Player.Disable();

        public void StartGame()
        {
            EnableInput();
            Time.timeScale = 1;
        }

        public static void RestartGame()
        {
            if (OnRestart != null) OnRestart.Invoke();
            DOTween.Clear();
        }

        private void PausedGame()
        {
            DisableInput();
            UnBind();
            Time.timeScale = 0;
        }
    }
}