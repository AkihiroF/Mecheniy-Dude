using _Source.Interactable.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.SavingEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class MedKitObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private MedicalKitSo medicalKit;
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
            InventoryPlayer.AddItem(medicalKit);
            Signals.Get<OnSaveStateObject>().Dispatch(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}