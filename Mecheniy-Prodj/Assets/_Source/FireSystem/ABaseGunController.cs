using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using _Source.SignalsEvents.WeaponsEvents;
using UnityEngine;

namespace _Source.FireSystem
{
    [RequireComponent(typeof(AudioWeaponController))]
    public abstract class ABaseGunController : MonoBehaviour, IPoolBullets
    {
        [SerializeField] private AudioWeaponController audioController;
        [SerializeField] private Transform pointExitBullet;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeReload;
        [SerializeField] private float speedAttack;

        public bool isAutomatic;

        public event Action<int> OnFireFromWeapon;
        public event Action OnDeleteBullets;

        private ClipSo _ammoInfo;
        private int _countAmmoInClip;
        protected int CurrentCountAmmoInGun;
        protected float SpeedBullet;
        protected float Damage;
        protected GameObject BulletObject;
        protected List<ABulletController> BulletPool;

        private bool _isMainReloading;
        private bool _isReloading;

        private bool _isFire;


        public void ReturnBulletInPool(ABulletController aBullet)
        {
            BulletPool.Add(aBullet);
        }

        public void SetParameters(ClipSo info,int countAmmo = 0, float upgradeSpeed = 0)
        {
            _countAmmoInClip = info.CountBullet;
            if (countAmmo == 0)
            {
                CurrentCountAmmoInGun = InventoryPlayer.UseItem(info, _countAmmoInClip);
            }
            else
            {
                CurrentCountAmmoInGun = countAmmo;
            }
            BulletObject = info.BulletObjectPrefab;
            SpeedBullet = info.SpeedBullet;
            Damage = info.Damage;
            _ammoInfo = info;
            BulletPool = new List<ABulletController>();
            UpgradeSpeedReloading(upgradeSpeed);
            _isMainReloading = false;
            Signals.Get<OnUpgradeSpeedReloading>().AddListener(UpgradeSpeedReloading);
            InvokeFireFromWeapon();
        }
        private void UpgradeSpeedReloading(float percent) => timeReload -= timeReload * percent / 100;

        protected abstract void InitialiseBullet();

        protected void SetPositionBullet(Transform bullet)
        {
            bullet.position = pointExitBullet.position;
            bullet.rotation = transform.rotation;
        }
        public void Fire()
        {
            if (isAutomatic)
            {
                _isFire = !_isFire;
            }
            if(_isMainReloading)
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
            if(CurrentCountAmmoInGun == _countAmmoInClip)
                return;
            _isMainReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.UseItem(_ammoInfo, _countAmmoInClip - CurrentCountAmmoInGun);
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
            audioController.PlayAudioReloading();
            yield return new WaitForSeconds(timeReload);
            CurrentCountAmmoInGun += countAmmo;
            Signals.Get<OnFinishReloadWeapon>().Dispatch();
            _isMainReloading = false;
            InvokeFireFromWeapon();
        }

        protected IEnumerator WaitFire()
        {
            audioController.PlayAudioFire();
            _isReloading = true;
            animator.SetTrigger("Fire");
            yield return new WaitForSeconds(speedAttack);
            _isReloading = false;
            if (isAutomatic & _isFire)
            {
                DoFire();
            }
        }

        private void OnDestroy()
        {
            Signals.Get<OnUpgradeSpeedReloading>().RemoveListener(UpgradeSpeedReloading);
            if (OnDeleteBullets != null) OnDeleteBullets.Invoke();
        }
    }
}