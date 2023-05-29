using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.UIEvents;
using UnityEngine;

namespace _Source.Interactable
{
    public class TerminalObject : MonoBehaviour, IInteractiveObject
    {

        public void Interact()
        {
            Signals.Get<OnEnableTerminal>().Dispatch();
            Signals.Get<OnPaused>().Dispatch();
        }
    }
}