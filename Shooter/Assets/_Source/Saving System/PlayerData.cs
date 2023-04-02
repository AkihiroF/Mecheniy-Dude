using System;
using System.Collections.Generic;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using UnityEngine;

namespace _Source.Saving_System
{
    [Serializable]
    public class PlayerData
    {
        public Dictionary<object, int> Inventory;
        public Vector2 position;
        public PlayerGunController currentGun;
         public PlayerHealth hp;
    }
}