using System;
using System.Collections.Generic;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Player
{
    public class SystemUpdating : MonoBehaviour
    {
        [SerializeField] private List<LvlUpgrading> lvlUpgradeSpeedMoving;
        [SerializeField] private Button upgradeSpeedMovingButton;
        [SerializeField] private TextMeshProUGUI textPriceSpeedMoving;
        [SerializeField] private TextMeshProUGUI textLvlSpeedMoving;
        
        [Space]
        
        [SerializeField] private List<LvlUpgrading> lvlUpgradeSpeedReloading;
        [SerializeField] private Button upgradeSpeedReloadingButton;
        [SerializeField] private TextMeshProUGUI textPriceSpeedReloading;
        [SerializeField] private TextMeshProUGUI textLvlSpeedReloading;
        
        [Space]
        
        [SerializeField] private List<LvlUpgrading> lvlUpgradeAngleVision;
        [SerializeField] private Button upgradeAngleVisionButton;
        [SerializeField] private TextMeshProUGUI textPriceAngleVision;
        [SerializeField] private TextMeshProUGUI textLvlAngleVision;
        
        [Space]
        
        [SerializeField] private TextMeshProUGUI textTotalScore;

        private int _currentLvlSpeedMoving;
        private int _currentLvlSpeedReloading;
        private int _currentLvlAngleVision;

        private int _currentScore;

        public int Score => _currentScore;

        public int LvlSpeedMoving => _currentLvlSpeedMoving;
        public int LvlSpeedReloading => _currentLvlSpeedReloading;
        public int LvlAngleVision => _currentLvlAngleVision;
        

        private void Start()
        {
            UpdateUI();
            Signals.Get<OnAddScoreUpgrade>().AddListener(AddScore);
        }

        private void AddScore(int score)
        {
            _currentScore += score;
            UpdateUI();
        }

        private void Bind()
        {
            
        }

        private void UpdateUI()
        {
            textTotalScore.text = $"{_currentScore}";
            if (_currentLvlSpeedMoving <= lvlUpgradeSpeedMoving.Count)
            {
                var speedMovingPrice = lvlUpgradeSpeedMoving[_currentLvlSpeedMoving].price;
                upgradeSpeedMovingButton.interactable =
                    _currentScore - speedMovingPrice >= 0;
                textPriceSpeedMoving.text = $"{speedMovingPrice}";
                textLvlSpeedMoving.text = $"{_currentLvlSpeedMoving}";
            }
            else
                upgradeSpeedMovingButton.interactable = false;
            
            if (_currentLvlSpeedReloading <= lvlUpgradeSpeedReloading.Count)
            {
                var speedReloadingPrice = lvlUpgradeSpeedReloading[_currentLvlSpeedReloading].price;
                upgradeSpeedReloadingButton.interactable =
                    _currentScore - speedReloadingPrice >= 0;
                textPriceSpeedReloading.text = $"{speedReloadingPrice}";
                textLvlSpeedReloading.text = $"{_currentLvlSpeedReloading}";
            }
            else
                upgradeSpeedReloadingButton.interactable = false;
            if (_currentLvlAngleVision <= lvlUpgradeAngleVision.Count)
            {
                var anglePrice = lvlUpgradeAngleVision[_currentLvlAngleVision].price;
                upgradeAngleVisionButton.interactable =
                    _currentScore - anglePrice >= 0;
                textPriceAngleVision.text = $"{anglePrice}";
                textLvlAngleVision.text = $"{_currentLvlAngleVision}";
            }
            else
                upgradeAngleVisionButton.interactable = false;

        }


        public void SetSavedData(int lvlSpeedMoving,
            int lvlSpeedReloading,int lvlAngleVision, int score)
        {
            _currentLvlAngleVision = lvlAngleVision;
            _currentScore = score;
            _currentLvlSpeedMoving = lvlSpeedMoving;
            _currentLvlSpeedReloading = lvlSpeedReloading;
            ApplySavedSpeedMoving();
            ApplySavedSpeedReloading();
            ApplySavedAngleVision();
        }

        private void ApplySavedSpeedMoving()
        {
            if(_currentLvlSpeedMoving == 0)
                return;
            var currentUpgrade = 0f;
            for (int i = 0; i < _currentLvlSpeedMoving; i++)
            {
                currentUpgrade += lvlUpgradeSpeedMoving[i].percentUpgrade;
            }
            Signals.Get<OnUpgradeSpeedMoving>().Dispatch(currentUpgrade);
        }
        private void ApplySavedSpeedReloading()
        {
            if(_currentLvlSpeedReloading == 0)
                return;
            var currentUpgrade = 0f;
            for (int i = 0; i < _currentLvlSpeedReloading; i++)
            {
                currentUpgrade += lvlUpgradeSpeedReloading[i].percentUpgrade;
            }
            Signals.Get<OnUpgradeSpeedReloading>().Dispatch(currentUpgrade);
        }
        private void ApplySavedAngleVision()
        {
            if(_currentLvlAngleVision == 0)
                return;
            var currentUpgrade = 0f;
            for (int i = 0; i < _currentLvlAngleVision; i++)
            {
                currentUpgrade += lvlUpgradeAngleVision[i].percentUpgrade;
            }
            Signals.Get<OnUpgradeAngleVision>().Dispatch(currentUpgrade);
        }

        private void OnDestroy()
        {
            Signals.Get<OnAddScoreUpgrade>().RemoveListener(AddScore);
        }
    }

    [Serializable]
    public class LvlUpgrading
    {
        public int price;
        public float percentUpgrade;
    }
}