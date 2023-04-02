using System;
using System.Collections.Generic;

namespace _Source.Player
{
    public static class InventoryPlayer
    {
        public static Dictionary<object, int> Inventory { get; private set; }

        static InventoryPlayer()
        {
            Inventory = new Dictionary<object, int>();
        }

        public static void SetInventory(Dictionary<object, int> savedInventory)
        {
            Inventory = savedInventory;
        }

        public static void AddItem(object typeObject, int count = 1)
        {
            try
            {
                Inventory[typeObject] += count;
            }
            catch
            {
                Inventory.Add(typeObject, count);
            }
        }

        public static int UseItem(object typeObject, int count = 1)
        {
            try
            {
                if (Inventory[typeObject] >= count)
                {
                    Inventory[typeObject] -= count;
                    return count;
                }
                else
                {
                    var currentValue = Inventory[typeObject];
                    Inventory[typeObject] = 0;
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
                return Inventory[typeObject];
            }
            catch
            {
                return -1;
            }
        }
    }
}