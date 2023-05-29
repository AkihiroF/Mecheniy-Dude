using System;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using UnityEngine;

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
    }
}