using _Source.FireSystem.Player;
using _Source.FireSystem.SOs;
using _Source.HealthSystem;
using _Source.InputSystem;
using _Source.Interactable;
using _Source.Interactable.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Space]
        [SerializeField] private PlayerMovement player;

        [SerializeField] private PlayerFireSystem playerFireSystem;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerInteractiveComponent playerInteractiveComponent;
        private void Awake()
        {
            var input = new Input();
            var inputHandler = new InputHandler(playerFireSystem, playerHealth, playerInteractiveComponent);
            player.SetInput(input);
            var game = new Game(input, inputHandler);
            game.StartGame();
        }
    }
}
