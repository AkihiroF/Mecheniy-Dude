using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.WeaponsEvents;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public abstract class ABaseGunController : MonoBehaviour, IPoolBullets
    {
        [SerializeField] private Transform pointExitBullet;
        [SerializeField] private float timeReload;
        [SerializeField] private float speedAttack;

        public event Action<int> OnFireFromWeapon;

        private ClipSo ammoInfo;
        private int countAmmoInClip;
        protected int CurrentCountAmmoInGun;
        protected float SpeedBullet;
        protected float Damage;
        protected GameObject BulletObject;
        protected List<ABulletController> BulletPool;

        private bool isMainReloading;
        private bool _isReloading;


        public void ReturnBulletInPool(ABulletController aBullet)
        {
            BulletPool.Add(aBullet);
        }

        public void SetParameters(ClipSo info,int countAmmo = 0)
        {
            countAmmoInClip = info.CountBullet;
            if (countAmmo == 0)
            {
                CurrentCountAmmoInGun = InventoryPlayer.UseItem(info, countAmmoInClip);
            }
            else
            {
                CurrentCountAmmoInGun = countAmmo;
            }
            BulletObject = info.BulletObjectPrefab;
            SpeedBullet = info.SpeedBullet;
            Damage = info.Damage;
            ammoInfo = info;
            BulletPool = new List<ABulletController>();

            isMainReloading = false;
            
            InvokeFireFromWeapon();
        }

        protected abstract void InitialiseBullet();

        protected void SetPositionBullet(Transform bullet)
        {
            bullet.position = pointExitBullet.position;
            bullet.rotation = transform.rotation;
        }
        public void Fire()
        {
            if(isMainReloading)
                return;
            if(_isReloading)
                return;
            DoFire();
        }

        protected abstract void DoFire();

        protected void UpdateCountAmmo()
        {
            CurrentCountAmmoInGun--;
            if (CurrentCountAmmoInGun == 0)
            {
                StartReloadWeapon();
            }
            InvokeFireFromWeapon();
        }

        private void InvokeFireFromWeapon()
        {
            if (OnFireFromWeapon != null)
                OnFireFromWeapon.Invoke(CurrentCountAmmoInGun);
        }

        public void StartReloadWeapon()
        {
            if(CurrentCountAmmoInGun == countAmmoInClip)
                return;
            isMainReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.UseItem(ammoInfo, countAmmoInClip - CurrentCountAmmoInGun);
            if (currentCountAmmoInInventory > 0)
            {
                Signals.Get<OnStartReloadWeapon>().Dispatch();
                StartCoroutine(ReloadWeapon(currentCountAmmoInInventory));
            }
            else
                InvokeFireFromWeapon();
        }

        private IEnumerator ReloadWeapon(int countAmmo)
        {
            yield return new WaitForSeconds(timeReload);
            CurrentCountAmmoInGun += countAmmo;
            Signals.Get<OnFinishReloadWeapon>().Dispatch();
            isMainReloading = false;
            InvokeFireFromWeapon();
        }

        protected IEnumerator WaitFire()
        {
            _isReloading = true;
            yield return new WaitForSeconds(speedAttack);
            _isReloading = false;
        }

        private void OnDestroy()
        {
            foreach (var bullet in BulletPool)
            {
                Destroy(bullet.gameObject);
            }
        }
    }
}