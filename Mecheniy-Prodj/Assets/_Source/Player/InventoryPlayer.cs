using System;
using System.Collections.Generic;
using _Source.FireSystem.SOs;

namespace _Source.Player
{
    public static class InventoryPlayer
    {
        public static Dictionary<int, int> Inventory { get; private set; }
        public static Dictionary<Type,PlayerGunSo> GunSos { get; private set; }

        static InventoryPlayer()
        {
            Inventory = new Dictionary<int, int>();
            GunSos = new Dictionary<Type, PlayerGunSo>();
        }

        public static void ClearInventory() => Inventory = new Dictionary<int, int>();

        #region UseObject

        public static void AddItem(object typeObject, int count = 1)
        {
            try
            {
                Inventory[typeObject.GetHashCode()] += count;
            }
            catch
            {
                Inventory.Add(typeObject.GetHashCode(), count);
            }
        }

        public static int UseItem(object typeObject, int count = 1)
        {
            try
            {
                if (Inventory[typeObject.GetHashCode()] >= count)
                {
                    Inventory[typeObject.GetHashCode()] -= count;
                    return count;
                }
                else
                {
                    var currentValue = Inventory[typeObject.GetHashCode()];
                    Inventory[typeObject.GetHashCode()] = 0;
                    return currentValue;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static int GetCountItem(object typeObject)
        {
            try
            {
                return Inventory[typeObject.GetHashCode()];
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region UseHashCode

        public static void AddItem(int hashObject, int count = 1)
        {
            if(count == 0)
                return;
            try
            {
                Inventory[hashObject] += count;
            }
            catch
            {
                Inventory.Add(hashObject, count);
            }
        }

        public static int UseItem(int hashObject, int count = 1)
        {
            try
            {
                if (Inventory[hashObject] >= count)
                {
                    Inventory[hashObject] -= count;
                    return count;
                }
                else
                {
                    var currentValue = Inventory[hashObject];
                    Inventory[hashObject] = 0;
                    return currentValue;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static int GetCountItem(int hashObject)
        {
            try
            {
                return Inventory[hashObject];
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region Weapon

        public static PlayerGunSo GetWeapon(Type type)
        {
            try
            {
                return GunSos[type];
            }
            catch
            {
                return null;
            }
        }

        public static void AddWeapon(Type type, PlayerGunSo playerGunSo)
        {
            try
            {
                GunSos.Add(type, playerGunSo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}