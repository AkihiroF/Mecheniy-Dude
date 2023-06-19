using _Source.Services;
using _Source.SignalsEvents.UIEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class TeleportObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private Transform pointTeleportation;
        [SerializeField] private LayerMask playerLayer;
        private Transform _player;
        
        public void Interact()
        {
            _player.position = pointTeleportation.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch("Нажмите 'Е', чтобы идти дальше",true);
                _player = other.transform;
            }
        } 
        private void OnTriggerExit2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch("",false);
                _player = null;
            }
        }
        
    }
}