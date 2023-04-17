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
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        private bool _isLoad;

        private void Awake()
        {
            BindButtons();
            _isLoad = PlayerPrefs.GetString(SaverData.NameData).Length == 0;
            if (_isLoad)
            {
                loadGameButton.enabled = false;
            }
        }

        private void BindButtons()
        {
            loadGameButton.onClick.AddListener(() => sceneLoader.LoadGame());
            startNewGameButton.onClick.AddListener(() => sceneLoader.LoadNewGame());
            quitButton.onClick.AddListener(() => sceneLoader.QuitGame());
        }
        
        
    }
}