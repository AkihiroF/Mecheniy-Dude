using System;
using System.Collections.Generic;
using System.Linq;
using _Source.FireSystem;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.SavingEvents;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.Saving_System
{
    public class PlayerSaverComponent : MonoBehaviour
    {
        public const string NameData = "PlayerData";
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerFireSystem playerFireSystem;
        [SerializeField] private SystemUpdating systemUpdating;

        private void Awake()
        {
            Signals.Get<OnSaving>().AddListener(SavePlayerData);
            if (PlayerPrefs.HasKey(NameData))
            {
                var nameSave = NameData;
                var data = PlayerPrefs.GetString(nameSave);
                if (data.Length != 0)
                {
                    var currentdata = JsonUtility.FromJson<PlayerData>(data);
                    for (int i = 0; i < currentdata.keysInventory.Count; i++)
                    {
                        InventoryPlayer.AddItem(currentdata.keysInventory[i], currentdata.valuesInventory[i]);
                    }

                    foreach (var gun in currentdata.guns)
                    {
                        var type = gun.GunObjectObject.GetComponent<ABaseGunComponent>().GetType();
                        InventoryPlayer.AddWeapon(type,gun);
                    }
                    health.SetSavedHeath(currentdata.hp);
                    if(currentdata.currentGun != null)
                    {
                        playerFireSystem.SetSavedParameters(currentdata.currentGun, currentdata.currentAmmoInGun);
                    }
                    transform.position = currentdata.position;
                    systemUpdating.SetSavedData(currentdata.lvlSpeedMoving,
                        currentdata.lvlSpeedReloading,currentdata.lvlAngleView, currentdata.countPointUpdate);
                }
            }
        }

        public void ClearData()
        {
            InventoryPlayer.ClearInventory();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Data is deleted");
        }

        private PlayerData GetPlayerData()
        {
            var inv = InventoryPlayer.Inventory;
            var guns = InventoryPlayer.GunSos;
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
                currentAmmoInGun = playerFireSystem.CurrentCountAmmoInGun,
                guns = guns.Values.ToList(),
                countPointUpdate = systemUpdating.Score,
                lvlAngleView = systemUpdating.LvlAngleVision,
                lvlSpeedReloading = systemUpdating.LvlSpeedReloading,
                lvlSpeedMoving = systemUpdating.LvlSpeedMoving
            };
        }

        private void SavePlayerData()
        {
            var data = GetPlayerData();
            string dataJson = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(NameData, dataJson);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            Signals.Get<OnSaving>().RemoveListener(SavePlayerData);
        }
    }
}