using System;
using _Source.InputSystem;
using _Source.Player;
using UnityEngine;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        private void Awake()
        {
            var input = new Input();
            player.SetInput(input);
            var inputHandler = new InputHandler(player);
            var game = new Game(input, inputHandler);
            game.StartGame();
        }
    }
}
