using _Source.InputSystem;

namespace _Source.Core
{
    public class Game
    {
        
        public Game(Input input, InputHandler inputHandler)
        {
            _input = input;
            _inputHandler = inputHandler;
            Bind();
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
        }
    }
}