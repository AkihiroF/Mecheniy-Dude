using System.Collections.Generic;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.InputSystem;
using _Source.Interactable;
using _Source.Player;
using UnityEngine;
using Zenject;

namespace _Source.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerFireSystem playerFireSystem;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerInteractiveComponent playerInteractiveComponent;
        [SerializeField] private List<Transform> pointForEnemies;
        public override void InstallBindings()
        {
            Container.Bind<Input>().AsCached().NonLazy();
            Container.Bind<PlayerMovement>().FromInstance(playerMovement);
            Container.Bind<PlayerFireSystem>().FromInstance(playerFireSystem);
            Container.Bind<PlayerHealth>().FromInstance(playerHealth);
            Container.Bind<PlayerInteractiveComponent>().FromInstance(playerInteractiveComponent);

            Container.Bind().To<PlayerMovement>().AsSingle().NonLazy();
            Container.Bind<InputHandler>().AsCached().NonLazy();
            Container.Bind<Game>().AsSingle().NonLazy();

            Debug.Log("enn");
        }
    }
}
