using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.Interactable
{
    public class AmmoObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private ClipSo typeAmmo;
        [SerializeField] private int countBullet;

        public ClipSo TypeAmmo
        {
            get
            {
                return typeAmmo;
            }
        }
        public void WakeUp()
        {
            InventoryPlayer.AddItem(typeAmmo,countBullet);
            Destroy(this.gameObject);
        }
    }
}