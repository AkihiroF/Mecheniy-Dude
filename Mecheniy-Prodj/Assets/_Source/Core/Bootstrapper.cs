using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.InputSystem;
using _Source.Interactable;
using _Source.Player;
using _Source.UI;
using UnityEngine;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerFireSystem playerFireSystem;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerInteractiveComponent playerInteractiveComponent;
        private void Awake()
        {
            var input = new Input();
            var inputHandler = new InputHandler(playerFireSystem, playerHealth, playerInteractiveComponent);
            playerMovement.SetInput(input);
            var game = new Game(input, inputHandler);
            game.StartGame();
        }
    }
}
