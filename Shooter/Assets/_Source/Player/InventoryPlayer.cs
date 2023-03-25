using System;
using System.Collections.Generic;

namespace _Source.Player
{
    public static class InventoryPlayer
    {
        private static Dictionary<object, int> _inventory;

        static InventoryPlayer()
        {
            _inventory = new Dictionary<object, int>();
        }

        public static void AddItem(object typeObject, int count = 1)
        {
            try
            {
                _inventory[typeObject] += count;
            }
            catch
            {
                _inventory.Add(typeObject, count);
            }
        }

        public static int UseItem(object typeObject, int count = 1)
        {
            try
            {
                if (_inventory[typeObject] >= count)
                {
                    _inventory[typeObject] -= count;
                    return count;
                }
                else
                {
                    var currentValue = _inventory[typeObject];
                    _inventory[typeObject] = 0;
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
                return _inventory[typeObject];
            }
            catch
            {
                return -1;
            }
        }
    }
}