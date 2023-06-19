using System;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using UnityEngine;

namespace _Source.Interactable
{
    public class AmmoObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private ClipSo typeAmmo;
        [SerializeField] private int countBullet;

        public ClipSo TypeAmmo => typeAmmo;

        public void Interact()
        {
            InventoryPlayer.AddItem(typeAmmo,countBullet);
            Destroy(this.gameObject);
        }
    }
}