using _Source.FireSystem.Player;
using _Source.HealthSystem;
using TMPro;
using UnityEngine;

namespace _Source.UI
{
    public class UiPreviewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textWeapon;
        [SerializeField] private GameObject panelReloading;
        [SerializeField] private GameObject medicalPanel;
        [SerializeField] private TextMeshProUGUI textMedical;

        private void Awake()
        {
            PlayerFireSystem.OnFire += PrintInfoAmmo;
            PlayerFireSystem.OnStartReloadWeapon += PrintReloading;
            PlayerFireSystem.OnFinishReloadWeapon += HideReloading;
            PlayerHealth.OnHealing += CheckKit;
            
            HideReloading();
            
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
    }
}