using _Source.FireSystem;
using _Source.FireSystem.Player;
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
            var type = gun.GunObjectObject.GetComponent<ABaseGunController>().GetType();
            InventoryPlayer.AddWeapon(type,gun);
            InventoryPlayer.AddItem(gun.ClipInfo, gun.ClipInfo.CountBullet);
            Destroy(this.gameObject);
        }
    }
}