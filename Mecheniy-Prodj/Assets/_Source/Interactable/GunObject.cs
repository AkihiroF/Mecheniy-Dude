using _Source.FireSystem;
using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.Interactable
{
    public class GunObject : MonoBehaviour,IInteractiveObject
    {
        [SerializeField] private PlayerGunSo gun;
        public void Interact()
        {
            var type = gun.GunObjectObject.GetComponent<ABaseGunComponent>().GetType();
            InventoryPlayer.AddWeapon(type,gun);
            Destroy(this.gameObject);
        }
    }
}