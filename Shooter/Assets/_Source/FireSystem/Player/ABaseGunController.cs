using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public abstract class ABaseGunController : MonoBehaviour
    {
        [SerializeField] private Transform pointExitBullet;
        [SerializeField] private float timeReload;
        [SerializeField] private float speedAttack;

        public event Action<int> OnFireFromWeapon; 
        
        protected ClipSo AmmoInfo;
        protected int CountAmmoInClip;
        protected int CurrentCountAmmoInGun;
        protected float SpeedBullet;
        protected float Damage;
        protected GameObject BulletObject;
        protected List<ABulletController> BulletPool;

        protected bool IsMainReloading;
        private bool _isReloading;


        public void ReturnBulletInPool(ABulletController aBullet)
        {
            BulletPool.Add(aBullet);
        }

        public void SetParameters(ClipSo info,int countAmmo = 0)
        {
            CountAmmoInClip = info.CountBullet;
            if (countAmmo == 0)
            {
                CurrentCountAmmoInGun = CountAmmoInClip;
            }
            else
            {
                CurrentCountAmmoInGun = countAmmo;
            }
            BulletObject = info.BulletObjectPrefab;
            SpeedBullet = info.SpeedBullet;
            Damage = info.Damage;
            AmmoInfo = info;
            BulletPool = new List<ABulletController>();

            IsMainReloading = false;
            
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
            if(IsMainReloading)
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
            if(CurrentCountAmmoInGun == CountAmmoInClip)
                return;
            IsMainReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.UseItem(AmmoInfo, CountAmmoInClip - CurrentCountAmmoInGun);
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
            CurrentCountAmmoInGun += countAmmo;
            PlayerFireSystem.FinishReloading();
            IsMainReloading = false;
            InvokeFireFromWeapon();
        }

        protected IEnumerator WaitFire()
        {
            _isReloading = true;
            yield return new WaitForSeconds(speedAttack);
            _isReloading = false;
        }
    }
}