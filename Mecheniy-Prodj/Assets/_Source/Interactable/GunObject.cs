using _Source.FireSystem;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.SavingEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class GunObject : MonoBehaviour,IInteractiveObject
    {
        [SerializeField] private PlayerGunSo gun;
        private void Awake()
        {
            Signals.Get<OnLoadStateObject>().AddListener(CheckLoad);
        }

        private void CheckLoad(int code)
        {
            if (this.GetHashCode() == code)
            {
                this.gameObject.SetActive(false);
            }
        }
        public void Interact()
        {
            var type = gun.GunObjectObject.GetComponent<ABaseGunComponent>().GetType();
            InventoryPlayer.AddWeapon(type,gun);
            Signals.Get<OnSaveStateObject>().Dispatch(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}