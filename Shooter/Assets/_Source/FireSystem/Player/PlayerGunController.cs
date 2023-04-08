using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        [SerializeField] private Transform pointExitBullet;
        [SerializeField] private float timeReload;

        public event Action<int> OnFireFromWeapon; 
        
        private ClipSo _ammoInfo;
        private int _countAmmoInClip;
        private int _currentCountAmmoInGun;
        private float _speedBullet;
        private float _damage;
        private GameObject _bulletObject;
        private List<BulletController> _bulletPool;

        private bool _isReloading;


        public void ReturnBulletInPool(BulletController bullet)
        {
            _bulletPool.Add(bullet);
        }

        public void SetParameters(ClipSo info,int countAmmo = 0)
        {
            _countAmmoInClip = info.CountBullet;
            if (countAmmo == 0)
            {
                _currentCountAmmoInGun = _countAmmoInClip;
            }
            else
            {
                _currentCountAmmoInGun = countAmmo;
            }
            _bulletObject = info.BulletPrefab;
            _speedBullet = info.SpeedBullet;
            _damage = info.Damage;
            _ammoInfo = info;
            _bulletPool = new List<BulletController>();

            _isReloading = false;
            
            InvokeFireFromWeapon();
        }

        private void InitialiseBullet()
        {
            if (_bulletPool.Count > 0)
            {
                var bullet = _bulletPool[0];
                SetPositionBullet(bullet.transform);
                bullet.FireBullet();
                _bulletPool.RemoveAt(0);
            }
            else
            {
                var newBullet = Instantiate(_bulletObject)
                    .GetComponent<BulletController>();
                newBullet.SetParameters(this, _speedBullet, _damage);
                SetPositionBullet(newBullet.transform);
                newBullet.FireBullet();
            }
        }

        private void SetPositionBullet(Transform bullet)
        {
            bullet.position = pointExitBullet.position;
            bullet.rotation = transform.rotation;
        }
        public void Fire()
        {
            if(_isReloading)
                return;
            if (_currentCountAmmoInGun > 0)
            {
                InitialiseBullet();
                UpdateCountAmmo();
            }
            else
            {
                StartReloadWeapon();
            }
        }

        private void UpdateCountAmmo()
        {
            _currentCountAmmoInGun--;
            if (_currentCountAmmoInGun == 0)
            {
                StartReloadWeapon();
            }
            InvokeFireFromWeapon();
        }

        private void InvokeFireFromWeapon()
        {
            if (OnFireFromWeapon != null)
                OnFireFromWeapon.Invoke(_currentCountAmmoInGun);
        }

        public void StartReloadWeapon()
        {
            if(_currentCountAmmoInGun == _countAmmoInClip)
                return;
            _isReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.UseItem(_ammoInfo, _countAmmoInClip - _currentCountAmmoInGun);
            if (currentCountAmmoInInventory > 0)
            {
                PlayerFireSystem.StartAutomaticReloading();
                StartCoroutine(ReloadWeapon(currentCountAmmoInInventory));
            }
            else
                InvokeFireFromWeapon();
        }

        private IEnumerator ReloadWeapon(int countAmmo)
        {
            yield return new WaitForSeconds(timeReload);
            _currentCountAmmoInGun += countAmmo;
            PlayerFireSystem.FinishReloading();
            _isReloading = false;
            InvokeFireFromWeapon();
        }
    }
}