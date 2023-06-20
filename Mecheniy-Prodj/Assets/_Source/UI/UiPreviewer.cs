using _Source.Core;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.HealthEvents;
using _Source.SignalsEvents.UIEvents;
using _Source.SignalsEvents.WeaponsEvents;
using DG.Tweening;
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
        [SerializeField] private Image iconWeapon;
        [SerializeField] private GameObject panelReloading;
        
        [Space]
        
        [SerializeField] private GameObject medicalPanel;
        [SerializeField] private TextMeshProUGUI textMedical;

        [Space] 
        
        [SerializeField] private Slider sliderHp;
        [SerializeField] private TextMeshProUGUI textHp;
        
        
        [Space]
        
        [SerializeField] private GameObject deadPanel;
        
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        
        [Space] 
        
        [SerializeField] private GameObject pausedPanel;
        
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button toMainMenuButton;

        [Space] 
        [SerializeField] private RectTransform weaponPanel;

        [SerializeField] private RectTransform startPoint;
        [SerializeField] private RectTransform endPoint;
        [SerializeField] private float timeTo;
        [SerializeField] private float timeFrom;

        [SerializeField] private GameObject weaponPanelObject;
        [SerializeField] private VerticalLayoutGroup listWeapon;


        [Space] 
        
        [SerializeField] private GameObject terminalPanel;
        [SerializeField] private Button closeTerminalButton;

        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private Button toUpgradeButton;
        [SerializeField] private Button closeUpgradeButton;

        [Space] 
        [SerializeField] private TextMeshProUGUI textToNextLvl;
        
        
        private void OnEnable()
        {
            Subscribe();
            BindButton();
            HideReloading();
            DisablePaused();
            DisableTerminal();
            DisableUpgrade();
            deadPanel.SetActive(false);
        }
        private void BindButton()
        {
            restartButton.onClick.AddListener((() =>
            {
                Signals.Get<OnRestart>().Dispatch();
                sceneLoader.LoadGame();
            }));
            menuButton.onClick.AddListener(() =>
            {
                UnBindButtons();
                Signals.Get<OnRestart>().Dispatch();
                sceneLoader.LoadMainMenu();
            });
            resumeButton.onClick.AddListener(() =>
            {
                DisablePaused();
                Signals.Get<OnResume>().Dispatch();
            });
            toMainMenuButton.onClick.AddListener(() =>
            {
                UnBindButtons();
                Signals.Get<OnRestart>().Dispatch();
                sceneLoader.LoadMainMenu();
            });
            
            closeTerminalButton.onClick.AddListener(() => DisableTerminal());
            toUpgradeButton.onClick.AddListener(() => EnableUpgrade());
            closeUpgradeButton.onClick.AddListener(() => DisableUpgrade());
        }

        private void UnBindButtons()
        {
            restartButton.onClick.RemoveAllListeners();
            menuButton.onClick.RemoveAllListeners();
            resumeButton.onClick.RemoveAllListeners();
            toMainMenuButton.onClick.RemoveAllListeners();
            
            closeTerminalButton.onClick.RemoveAllListeners();
            toUpgradeButton.onClick.RemoveAllListeners();
            closeUpgradeButton.onClick.RemoveAllListeners();
        }

        private void Subscribe()
        {
            Signals.Get<OnPrintInfoAboutFire>().AddListener(PrintInfoAmmo);
            Signals.Get<OnStartReloadWeapon>().AddListener(PrintReloading);
            Signals.Get<OnFinishReloadWeapon>().AddListener(HideReloading);
            Signals.Get<OnUpdateIconWeapon>().AddListener(UpdateIconWeapon);
            
            Signals.Get<OnHealing>().AddListener(CheckKit);
            Signals.Get<OnDead>().AddListener(PrintDead);
            Signals.Get<OnDamagePlayer>().AddListener(ShowHp);
            
            Signals.Get<OnEnablePaused>().AddListener(EnablePaused);
            Signals.Get<OnRestart>().AddListener(UnSubscribe);
            
            Signals.Get<OnEnableTableWeapon>().AddListener(SwitchStateWeaponPanel);
            Signals.Get<OnAddWeaponToPanel>().AddListener(AddWeaponToPanel);
            
            Signals.Get<OnEnableTerminal>().AddListener(EnableTerminal);
            
            Signals.Get<OnShowToNextLvl>().AddListener(SwitchStateNextLvl);
        }

        private void UnSubscribe()
        {
            Signals.Get<OnPrintInfoAboutFire>().RemoveListener(PrintInfoAmmo);
            Signals.Get<OnStartReloadWeapon>().RemoveListener(PrintReloading);
            Signals.Get<OnFinishReloadWeapon>().RemoveListener(HideReloading);
            Signals.Get<OnUpdateIconWeapon>().RemoveListener(UpdateIconWeapon);
            
            Signals.Get<OnHealing>().RemoveListener(CheckKit);
            Signals.Get<OnDead>().RemoveListener(PrintDead);
            Signals.Get<OnDamagePlayer>().RemoveListener(ShowHp);
            
            Signals.Get<OnEnablePaused>().RemoveListener(EnablePaused);
            Signals.Get<OnRestart>().RemoveListener(UnSubscribe);
            
            Signals.Get<OnEnableTableWeapon>().RemoveListener(SwitchStateWeaponPanel);
            Signals.Get<OnAddWeaponToPanel>().RemoveListener(AddWeaponToPanel);
            
            Signals.Get<OnEnableTerminal>().RemoveListener(EnableTerminal);
            
            Signals.Get<OnShowToNextLvl>().RemoveListener(SwitchStateNextLvl);
        }

        private void SwitchStateNextLvl(string info,bool isActive)
        {
            textToNextLvl.DOComplete();

            if (isActive)
            {
                textToNextLvl.text = info;
                textToNextLvl.DOFade(1, 1);
            }
            else
            {
                textToNextLvl.DOFade(0, 0);
            }
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
                private void UpdateIconWeapon(Sprite obj)
                {
                    iconWeapon.sprite = obj;
                }

                private void SwitchStateWeaponPanel(bool isActive)
                {
                    weaponPanel.DOComplete();
                    var endPosition = isActive ? endPoint.position : startPoint.position;
                    var time = isActive ? timeTo : timeFrom;
                    weaponPanel.DOMove(endPosition, time);
                }

                private void AddWeaponToPanel(Sprite icon)
                {
                    var size = weaponPanelObject.GetComponent<RectTransform>().sizeDelta;
                    var transformPanel = listWeapon.GetComponent<RectTransform>();
                    var newSizePanel = transformPanel.sizeDelta + size + new Vector2(0, listWeapon.spacing);
                    transformPanel.sizeDelta = newSizePanel;
                    var newWeapon = Instantiate(weaponPanelObject, listWeapon.transform);
                    newWeapon.GetComponent<Image>().sprite = icon;
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

        private void ShowHp(float hp)
        {
            var currentValue = sliderHp.value;
            DOTween.To(() => currentValue, x => currentValue = x, hp, 1f)
                .OnUpdate(() => sliderHp.value = currentValue);
            textHp.text = $"{Mathf.FloorToInt(hp)}";
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

        #region Terminal

        private void EnableTerminal()
        {
            terminalPanel.SetActive(true);
        }

        private void DisableTerminal()
        {
            terminalPanel.SetActive(false);
            Signals.Get<OnResume>().Dispatch();
        }

        private void EnableUpgrade()
        {
            upgradePanel.SetActive(true);
        }

        private void DisableUpgrade()
        {
            upgradePanel.SetActive(false);
        }

        #endregion
    }
}