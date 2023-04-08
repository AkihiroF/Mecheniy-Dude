using System;
using _Source.Core;
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

        private PlayerGunSo _currentGunSo;
        private GameObject _gunObj;
        private PlayerGunController _currentGun;
        private ClipSo _currentClip;
        private int _currentCountAmmo;

        private void Start()
        {
            Game.OnRestart += UnSubscribe;
            OnFinishReloadWeapon += PrintAmmo;
            if (_currentGunSo == null)
            {
                _currentGunSo = firstGun;
            }
            _gunObj = Instantiate(_currentGunSo.GunObject, pointPositionGun);
            _currentGun = _gunObj.GetComponent<PlayerGunController>();
            _currentGun.OnFireFromWeapon += UpdateCurrentCountAmmoInGun;
            _currentClip = _currentGunSo.ClipInfo;
            SetParamInGun();
        }

        private void UnSubscribe()
        {
            _currentGun.OnFireFromWeapon -= UpdateCurrentCountAmmoInGun;
            OnFinishReloadWeapon -= PrintAmmo;
            Game.OnRestart -= UnSubscribe;
        }

        public void SetSavedParameters(PlayerGunSo savedGun, int currentAmmo)
        {
            _currentGunSo = savedGun;
            _currentCountAmmo = currentAmmo;
        }

        public PlayerGunSo GetCurrentGun
        {
            get
            {
                return _currentGunSo;
            }
        }

        public int CurrentCountAmmoInGun
        {
            get
            {
                return _currentCountAmmo;
            }
        }

        private void SetParamInGun()
        {
            _currentGun.SetParameters(_currentClip,_currentCountAmmo);
        }

        private void UpdateCurrentCountAmmoInGun(int count)
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
