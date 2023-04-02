using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Interactable;
using _Source.Player;
using UnityEngine;

namespace _Source.Saving_System
{
    public class SaverData : MonoBehaviour, IInteractiveObject
    {
        public const string NameData = "PlayerData";
        public static PlayerData PlayerData { get; private set; }
        public SaverData()
        {
            if (SaverData.PlayerData != null)
            {
                PlayerData = SaverData.PlayerData;
            }
            var data = PlayerPrefs.GetString(NameData);
            if (data.Length != 0)
            {
                PlayerData = JsonUtility.FromJson<PlayerData>(data);
            }
        }
        public void SaveData(Transform playerTransform, PlayerGunController currentGun, PlayerHealth playerHealth)
        {
            PlayerData.currentGun = currentGun;
            PlayerData.Inventory = InventoryPlayer.Inventory;
            PlayerData.position = playerTransform.position;
            string dataJson = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(NameData, dataJson);
            PlayerPrefs.Save();
        }

        public void WakeUp()
        {
            
        }
    }
}