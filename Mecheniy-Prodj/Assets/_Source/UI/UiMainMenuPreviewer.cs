using _Source.Saving_System;
using _Source.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class UiMainMenuPreviewer : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button startNewGameButton;
        
        [SerializeField] private Button loadFirstLvl;
        [SerializeField] private Button loadSecondLvl;
        [SerializeField] private Button loadThirdLvl;
        
        [SerializeField] private Button settingsButton;

        private bool _isLoad;

        private void Awake()
        {
            BindButtons();
            _isLoad = PlayerPrefs.GetString(PlayerSaverComponent.NameData).Length == 0;
            if (_isLoad)
            {
                loadGameButton.enabled = false;
            }
        }

        private void BindButtons()
        {
            loadGameButton.onClick.AddListener(() => sceneLoader.LoadGame());
            startNewGameButton.onClick.AddListener(() => sceneLoader.LoadNewGame());
            loadFirstLvl.onClick.AddListener(() => sceneLoader.LoadFirsLvl());
            loadSecondLvl.onClick.AddListener(() => sceneLoader.LoadSecondLvl());
            loadThirdLvl.onClick.AddListener(() => sceneLoader.LoadThirdLvl());
        }
        
        
    }
}