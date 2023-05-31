using System;
using System.Collections.Generic;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Interactable
{
    public class SystemMessengerObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private List<PanelMessenger> massages;

        private void Awake()
        {
            Bind();
            mainPanel.SetActive(false);
            foreach (var massage in massages)
            {
                massage.panelWithText.SetActive(false);
            }
        }

        private void Bind()
        {
            for (int i = 0; i < massages.Count; i++)
            {
                if (i == massages.Count - 1)
                {
                    massages[i].toNextMessageButton.onClick.AddListener(() => CloseMessenger());
                    return;
                }

                var i1 = i;
                massages[i].toNextMessageButton.onClick.AddListener(() =>
                {
                    ToNextMessage(i1);
                });
            }
        }

        private void ToNextMessage(int id)
        {
            massages[id].panelWithText.SetActive(false);
            massages[id+1].panelWithText.SetActive(true);
        }

        public void Interact()
        {
            mainPanel.SetActive(true);
            EnableFirstMessage();
            Signals.Get<OnPaused>().Dispatch();
        }

        private void CloseMessenger()
        {
            mainPanel.SetActive(false);
            Signals.Get<OnResume>().Dispatch();
        }

        private void EnableFirstMessage()
        {
            massages[0].panelWithText.SetActive(true);
        }
    }

    [Serializable]
    public class PanelMessenger
    {
        public GameObject panelWithText;
        public Button toNextMessageButton;
    }
}