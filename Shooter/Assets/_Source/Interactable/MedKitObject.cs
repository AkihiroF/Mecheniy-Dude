using _Source.Interactable.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.Interactable
{
    public class MedKitObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private MedicalKitSo medicalKit;
        public void WakeUp()
        {
            InventoryPlayer.AddItem(medicalKit);
            Destroy(this.gameObject);
        }
        
    }
}