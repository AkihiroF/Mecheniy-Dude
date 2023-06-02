using _Source.Interactable;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour , IInteractiveObject
{
    [SerializeField] private GameObject panelFinish;
    public void Interact()
    {
        Signals.Get<OnPaused>().Dispatch();
        panelFinish.SetActive(true);
    }
}
