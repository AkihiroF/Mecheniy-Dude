using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.UIEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class TerminalObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private LayerMask playerLayer;
        public void Interact()
        {
            Signals.Get<OnEnableTerminal>().Dispatch();
            Signals.Get<OnPaused>().Dispatch();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch("Нажмите 'Е' чтобы открыть терминал",true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch(" ",false);
            }
        }
    }
}