using System;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.SavingEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class AmmoObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private ClipSo typeAmmo;
        [SerializeField] private int countBullet;

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

        public ClipSo TypeAmmo => typeAmmo;

        public void Interact()
        {
            InventoryPlayer.AddItem(typeAmmo,countBullet);
            Signals.Get<OnSaveStateObject>().Dispatch(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}