using _Source.InputSystem;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.HealthEvents;
using _Source.SignalsEvents.WeaponsEvents;
using DG.Tweening;
using UnityEngine;

namespace _Source.Core
{
    public class Game
    {
        public Game(Input input, InputHandler inputHandler)
        {
            _input = input;
            _inputHandler = inputHandler;
            Subscribe();
        }

        private Input _input;
        private InputHandler _inputHandler;
        private bool _isAutomatic;

        private void Subscribe()
        {
            Signals.Get<OnDead>().AddListener(PausedGame);
            Signals.Get<OnSwitchFireMode>().AddListener(SwitchFireMode);
            Signals.Get<OnPaused>().AddListener(PausedGame);
            Signals.Get<OnResume>().AddListener(StartGame);
            Signals.Get<OnRestart>().AddListener(RestartGame);
        }

        private void UnSubscribe()
        {
            Signals.Get<OnDead>().RemoveListener(PausedGame);
            Signals.Get<OnSwitchFireMode>().RemoveListener(SwitchFireMode);
            Signals.Get<OnPaused>().RemoveListener(PausedGame);
            Signals.Get<OnResume>().RemoveListener(StartGame);
            Signals.Get<OnRestart>().RemoveListener(RestartGame);
        }

        private void Bind()
        {
            var input = _input.Player;
            
            input.Fire.performed += _inputHandler.InputFire;
            input.FireAutomatic.performed += _inputHandler.InputFire;
            
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
            input.FireAutomatic.performed -= _inputHandler.InputFire;
            
            input.ChooseKnife.performed -= _inputHandler.InputChooseKnife;
            input.ChoosePistol.performed -= _inputHandler.InputChoosePistol;
            input.ChooseShortGun.performed -= _inputHandler.InputChooseShortGun;
            input.ChooseRifle.performed -= _inputHandler.InputChooseRifle;
            
            input.Reload.performed -= _inputHandler.InputReload;
            input.Healing.performed -= _inputHandler.InputHealing;
            
            _input.Interface.Paused.performed -= _inputHandler.InputPaused;
        }
        private void EnablePlayerInput() 
            => _input.Player.Enable();

        private void DisablePlayerInput()
            => _input.Player.Disable();

        private void SwitchFireMode(bool isAutomatic)
        {
            _isAutomatic = isAutomatic;
            SetFireMode();
        }

        private void SetFireMode()
        {
            if (_isAutomatic)
            {
                _input.Player.Fire.Disable();
                _input.Player.FireAutomatic.Enable();
            }
            else
            {
                _input.Player.Fire.Enable();
                _input.Player.FireAutomatic.Disable();
            }
        }
        public void StartGame()
        {
            EnablePlayerInput();
            Bind();
            SetFireMode();
            Time.timeScale = 1;
            _input.Interface.Enable();
        }

        private void RestartGame()
        {
            DOTween.Clear();
            UnSubscribe();
        }

        private void PausedGame()
        {
            UnBind();
            DisablePlayerInput();
            Time.timeScale = 0;
        }
    }
}