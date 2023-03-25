using System;
using _Source.FireSystem.SOs;
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

        private void Start()
        {
            if (_currentGun == null)
            {
                _gunObj = Instantiate(firstGun.GunObject, pointPositionGun);
                _currentGun = _gunObj.GetComponent<PlayerGunController>();
                _currentGun.OnFireFromWeapon += PrintAmmo;
                SetParamInGun();
            }
        }

        private void SetParamInGun()
        {
            _currentGun.SetParameters(firstGun.ClipInfo);
        }

        private void PrintAmmo(int countAmmo, int countClip)
        {
            if (OnFire != null)
            {
                OnFire.Invoke($"{countAmmo} / {countClip}");
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
