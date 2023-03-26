using System;
using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public class PlayerFireSystem : MonoBehaviour
    {
        [SerializeField] private Transform pointPositionGun;
        [SerializeField] private PlayerGunSo firstGun;

        public static event Action<string> OnFire;
        public static event Action OnSwitchWeapon;
        public static event Action OnStartReloadWeapon;
        public static event Action OnFinishReloadWeapon;
        
        
        private GameObject _gunObj;
        private PlayerGunController _currentGun;
        private ClipSo _currentClip;
        private int _currentCountAmmo;

        private void Start()
        {
            if (_currentGun == null)
            {
                _gunObj = Instantiate(firstGun.GunObject, pointPositionGun);
                _currentGun = _gunObj.GetComponent<PlayerGunController>();
                _currentGun.OnFireFromWeapon += SetCurrentCountAmmoInGun;
                _currentClip = firstGun.ClipInfo;
                SetParamInGun();
            }

            OnFinishReloadWeapon += PrintAmmo;
        }

        private void SetParamInGun()
        {
            _currentGun.SetParameters(_currentClip);
        }

        private void SetCurrentCountAmmoInGun(int count)
        {
            _currentCountAmmo = count;
            PrintAmmo();
        }


        public void PrintAmmo()
        {
            if (OnFire != null)
            {
                OnFire.Invoke($"{_currentCountAmmo} / {InventoryPlayer.GetCountItem(_currentClip) / _currentClip.CountBullet}");
            }
        }

        public void Fire()
        {
            _currentGun.Fire();
        }


        public void ReloadWeapon()
        {
            _currentGun.StartReloadWeapon();
        }

        public static void StartAutomaticReloading()
        {
            if (OnStartReloadWeapon != null) OnStartReloadWeapon.Invoke();
        }
        public static void FinishReloading()
        {
            if (OnFinishReloadWeapon != null) OnFinishReloadWeapon.Invoke();
        }
    }
}
