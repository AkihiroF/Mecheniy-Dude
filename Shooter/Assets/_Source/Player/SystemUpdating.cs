using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.Player
{
    public class SystemUpdating : MonoBehaviour
    {
        [SerializeField] private List<LvlUpdating> lvls;
    }

    [Serializable]
    public class LvlUpdating
    {
        public TypeUpdating TypeUpdating;
        public float percentUpgrade;
        public int lvl;
    }

    public enum TypeUpdating
    {
        SpeedMoving = 0,
        SpeedReloading = 1,
        RadiusView = 2
    }
}