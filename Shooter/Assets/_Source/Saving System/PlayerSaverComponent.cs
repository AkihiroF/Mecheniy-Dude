using System.Collections.Generic;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Player;
using UnityEngine;

namespace _Source.Saving_System
{
    public class PlayerSaverComponent : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerFireSystem playerFireSystem;

        private void Awake()
        {
            var nameSave = SaverData.NameData;
            var data = PlayerPrefs.GetString(nameSave);
            if (data.Length != 0)
            {
                var currentdata = JsonUtility.FromJson<PlayerData>(data);
                for (int i = 0; i < currentdata.keysInventory.Count; i++)
                {
                    InventoryPlayer.AddItem(currentdata.keysInventory[i], currentdata.valuesInventory[i]);
                }
                health.SetSavedHeath(currentdata.hp);
                if(currentdata.currentGun != null)
                    playerFireSystem.SetSavedParameters(currentdata.currentGun, currentdata.currentAmmoInGun);
                transform.position = currentdata.position;
            }
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Data is deleted");
        }

        public PlayerData GetPlayerData()
        {
            var inv = InventoryPlayer.Inventory;
            var keys = new List<int>();
            var values = new List<int>();
            foreach (var key in inv.Keys)
            {
                keys.Add(key);
                values.Add(inv[key]);
            }
            
            return new PlayerData()
            {
                currentGun = playerFireSystem.GetCurrentGun,
                hp = health.GetHp,
                keysInventory = keys,
                valuesInventory = values,
                position = transform.position,
                currentAmmoInGun = playerFireSystem.CurrentCountAmmoInGun
            };
        }
    }
}