using System;
using _Source.Saving_System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class UiMainMenuPreviewer : MonoBehaviour
    {
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button settingsButton;

        private bool _isLoad;

        private void Awake()
        {
            _isLoad = PlayerPrefs.GetString(SaverData.NameData).Length != 0;
            if (_isLoad)
            {
                loadGameButton.enabled = false;
            }
        }
        
        
    }
}