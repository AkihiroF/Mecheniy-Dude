using System;
using _Source.Services;
using _Source.SignalsEvents;
using _Source.SignalsEvents.CoreEvents;
using UnityEngine;
using UnityEngine.UI;

namespace _Source
{
    public class FinishGame : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private GameObject panelVin;
        [SerializeField] private Button toMainMenuButton;
        private int _currentCountEnemy;

        private void Awake()
        {
            Signals.Get<OnDeadEnemy>().AddListener(CheckEnemy);
            toMainMenuButton.onClick.AddListener(() => sceneLoader.LoadMainMenu());
        }

        private void CheckEnemy(bool isDead)
        {
            if (isDead)
                _currentCountEnemy--;
            else
                _currentCountEnemy++;
            if (_currentCountEnemy == 0)
            {
                ShowVictory();
            }
        }

        private void ShowVictory()
        {
            Signals.Get<OnDeadEnemy>().RemoveListener(CheckEnemy);
            Signals.Get<OnPaused>().Dispatch();
            panelVin.SetActive(true);
        }
    }
}