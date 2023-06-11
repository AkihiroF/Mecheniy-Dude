using System.Collections.Generic;
using _Source.Enemy;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Interactable;
using _Source.Player;
using UnityEngine;

namespace _Source.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerFireSystem playerFireSystem;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerInteractiveComponent playerInteractiveComponent;

        [SerializeField] private List<Transform> points;

        private void Awake()
        {
            // var input = new Input();
            // var inputHandler = new InputHandler(playerFireSystem, playerHealth, playerInteractiveComponent);
            // playerMovement.SetInput(input);
            // new Game(input, inputHandler);
            // Signals.Get<OnResume>().Dispatch();
            // //game.StartGame();
            CreatorDirectoryEnemy.PositionsPoints.AddPoints(points);
        }
    }
}
