using _Source.Interactable;
using UnityEngine;

namespace _Source.Saving_System
{
    public class SaverData : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private PlayerSaverComponent saverComponent;
        public const string NameData = "PlayerData";

        private void SaveData()
        {
            var data = saverComponent.GetPlayerData();
            string dataJson = JsonUtility.ToJson(data);
            Debug.Log(dataJson);
            PlayerPrefs.SetString(NameData, dataJson);
            PlayerPrefs.Save();
        }

        public void Interact()
        {
            SaveData();
        }
    }
}