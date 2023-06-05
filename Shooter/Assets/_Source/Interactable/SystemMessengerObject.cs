using System;
using System.Collections.Generic;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.UIEvents;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Interactable
{
    public class SystemMessengerObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private List<PanelMessenger> massages;
        [SerializeField] private LayerMask playerLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch("Нажмите 'Е' чтобы прочитать сообщение",true);
            }
        } 
        private void OnTriggerExit2D(Collider2D other)
        {
            if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Signals.Get<OnShowToNextLvl>().Dispatch("",false);
            }
        }

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
            massages[massages.Count-1].panelWithText.SetActive(false);
            mainPanel.SetActive(false);
            Signals.Get<OnResume>().Dispatch();
        }

        private void EnableFirstMessage()
        {
            massages[0].panelWithText.SetActive(true);
        }

        private void OnDestroy()
        {
            foreach (var massage in massages)
            {
                massage.toNextMessageButton.onClick.RemoveAllListeners();
            }
        }
    }

    [Serializable]
    public class PanelMessenger
    {
        public GameObject panelWithText;
        public Button toNextMessageButton;
    }
}