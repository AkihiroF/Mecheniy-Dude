using _Source.InputSystem;
using _Source.Player;

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
            var movement = _input.Player.Moving;
            movement.performed += _inputHandler.MoveAction;
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