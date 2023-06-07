using System;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.Saving_System
{
    [Serializable]
    public struct PlayerData
    {
        public List<int> keysInventory;
        public List<int> valuesInventory;
        public Vector2 position;
        public PlayerGunSo currentGun;
         public float hp;
         public int currentAmmoInGun;
         public List<PlayerGunSo> guns;
         public int countPointUpdate;
         public int lvlSpeedMoving;
         public int lvlSpeedReloading;
         public int lvlAngleView;
    }
}