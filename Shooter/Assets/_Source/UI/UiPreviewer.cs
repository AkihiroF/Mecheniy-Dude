using _Source.Core;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class UiPreviewer : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [Space]
        [SerializeField] private TextMeshProUGUI textWeapon;
        [SerializeField] private GameObject panelReloading;
        [Space]
        [SerializeField] private GameObject medicalPanel;
        [SerializeField] private TextMeshProUGUI textMedical;
        [Space]
        [SerializeField] private GameObject deadPanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [Space] 
        [SerializeField] private GameObject pausedPanel;

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button loadLastGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button toMainMenuButton;


        private Game _game;
        private void Awake()
        {
            Subscribe();
            BindButton();
            HideReloading();
            DisablePaused();
            deadPanel.SetActive(false);
        }

        public void SetGame(Game game)
            => _game = game;
        private void BindButton()
        {
            restartButton.onClick.AddListener((() =>
            {
                _game.RestartGame();
                sceneLoader.LoadGame();
            }));
            menuButton.onClick.AddListener(() =>
            {
                UnBindButtons();
                _game.RestartGame();
                sceneLoader.LoadMainMenu();
            });
            resumeButton.onClick.AddListener(() =>
            {
                DisablePaused();
                _game.StartGame();
            });
            loadLastGameButton.onClick.AddListener(() =>
            {
                _game.RestartGame();
                sceneLoader.LoadGame();
            });
            toMainMenuButton.onClick.AddListener(() =>
            {
                UnBindButtons();
                _game.RestartGame();
                sceneLoader.LoadMainMenu();
            });
        }

        private void UnBindButtons()
        {
            restartButton.onClick.RemoveAllListeners();
            menuButton.onClick.RemoveAllListeners();
            resumeButton.onClick.RemoveAllListeners();
            loadLastGameButton.onClick.RemoveAllListeners();
            toMainMenuButton.onClick.RemoveAllListeners();
        }

        private void Subscribe()
        {
            PlayerFireSystem.OnFire += PrintInfoAmmo;
            PlayerFireSystem.OnStartReloadWeapon += PrintReloading;
            PlayerFireSystem.OnFinishReloadWeapon += HideReloading;
            PlayerHealth.OnHealing += CheckKit;
            PlayerHealth.OnDead += PrintDead;
            Game.OnRestart += UnSubscribe;
            Game.OnPaused += EnablePaused;
        }

        private void UnSubscribe()
        {
            PlayerFireSystem.OnFire -= PrintInfoAmmo;
            PlayerFireSystem.OnStartReloadWeapon -= PrintReloading;
            PlayerFireSystem.OnFinishReloadWeapon -= HideReloading;
            PlayerHealth.OnHealing -= CheckKit;
            PlayerHealth.OnDead -= PrintDead;
            Game.OnRestart -= UnSubscribe;
            Game.OnPaused -= EnablePaused;
        }

        #region Weapon

                private void PrintInfoAmmo(string text)
                {
                    textWeapon.text = text;
                }
                private void PrintReloading()
                {
                    panelReloading.SetActive(true);
                }
                private void HideReloading()
                {
                    panelReloading.SetActive(false);
                }

        #endregion

        #region Healh

        private void CheckKit(int count)
        {
            if (count > 0)
            {
                medicalPanel.SetActive(true);
                textMedical.text = $"{count}";
            }
            else
            {
                medicalPanel.SetActive(false);
            }
        }

        #endregion

        #region Dead

        private void PrintDead()
        {
            deadPanel.SetActive(true);
        }

        #endregion

        #region Paused

        private void EnablePaused()
        {
            pausedPanel.SetActive(true);
        }

        private void DisablePaused()
        {
            pausedPanel.SetActive(false);
        }

        #endregion
    }
}