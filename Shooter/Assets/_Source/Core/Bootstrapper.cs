using _Source.FireSystem.Player;
using _Source.InputSystem;
using _Source.Player;
using UnityEngine;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Space]
        [SerializeField] private PlayerMovement player;

        [SerializeField] private PlayerFireSystem playerFireSystem;
        private void Awake()
        {
            var input = new Input();
            var inputHandler = new InputHandler(playerFireSystem);
            player.SetInput(input);
            var game = new Game(input, inputHandler);
            game.StartGame();
        }
    }
}
