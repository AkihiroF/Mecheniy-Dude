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