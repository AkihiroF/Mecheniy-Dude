using _Source.Core;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Awake()
        {
            PlayerFireSystem.OnFire += PrintInfoAmmo;
            PlayerFireSystem.OnStartReloadWeapon += PrintReloading;
            PlayerFireSystem.OnFinishReloadWeapon += HideReloading;
            PlayerHealth.OnHealing += CheckKit;
            PlayerHealth.OnDead += PrintDead;
            HideReloading();

            Game.OnRestart += UnSubscribe;
            
            restartButton.onClick.AddListener((() =>
            {
                Game.RestartGame();
                sceneLoader.LoadGame();
            }));
            menuButton.onClick.AddListener(() =>
            {
                Game.RestartGame();
                sceneLoader.LoadMainMenu();
            });
            deadPanel.SetActive(false);
        }

        private void UnSubscribe()
        {
            PlayerFireSystem.OnFire -= PrintInfoAmmo;
            PlayerFireSystem.OnStartReloadWeapon -= PrintReloading;
            PlayerFireSystem.OnFinishReloadWeapon -= HideReloading;
            PlayerHealth.OnHealing -= CheckKit;
            PlayerHealth.OnDead -= PrintDead;
            Game.OnRestart -= UnSubscribe;
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
    }
}