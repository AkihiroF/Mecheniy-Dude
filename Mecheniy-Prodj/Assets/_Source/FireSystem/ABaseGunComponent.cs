using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using _Source.SignalsEvents.WeaponsEvents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.FireSystem
{
    [RequireComponent(typeof(AudioWeaponComponent))]
    public abstract class ABaseGunComponent : MonoBehaviour, IPoolBullets
    {
        [SerializeField] private AudioWeaponComponent audioComponent;
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
        protected List<ABulletComponent> BulletPool;

        private Tween _tweenReloading;
        private Tween _tweenFire;

        private bool _isActive;
        protected bool IsMainReloading;
        private bool _isReloading;

        private bool _isFire;

        private bool _isEnemy;


        public void ReturnBulletInPool(ABulletComponent aBullet)
        {
            BulletPool.Add(aBullet);
        }

        public void SetParameters(ClipSo info,int countAmmo = 0, float upgradeSpeed = 0, bool isEnemy = false)
        {
            _countAmmoInClip = info.CountBullet;
            CurrentCountAmmoInGun = countAmmo == 0 ? _countAmmoInClip : countAmmo;
            BulletObject = info.BulletObjectPrefab;
            SpeedBullet = info.SpeedBullet;
            Damage = info.Damage;
            _ammoInfo = info;
            BulletPool = new List<ABulletComponent>();
            UpgradeSpeedReloading(upgradeSpeed);
            IsMainReloading = false;
            if(isEnemy == false)
                Signals.Get<OnUpgradeSpeedReloading>().AddListener(UpgradeSpeedReloading);
            _isEnemy = isEnemy;
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
            if(IsMainReloading)
                return;
            if(_isReloading)
                return;
            DoFire();
        }

        protected abstract void DoFire();

        private void OnEnable()
        {
            _isActive = true;
        }


        public void OnSwitchedWeapon()
        {
            if (IsMainReloading == false)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                _isActive = false;
            }
        }

        protected void UpdateCountAmmo()
        {
            CurrentCountAmmoInGun--;
            if (CurrentCountAmmoInGun == 0)
            {
                StartReloadWeapon();
            }
            InvokeFireFromWeapon();
        }

        public void InvokeFireFromWeapon()
        {
            if (OnFireFromWeapon != null)
                OnFireFromWeapon.Invoke(CurrentCountAmmoInGun);
        }

        public void StartReloadWeapon()
        {
            if(IsMainReloading)
                return;
            if(CurrentCountAmmoInGun == _countAmmoInClip)
                return;
            IsMainReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.GetCountItem(_ammoInfo);
            if (currentCountAmmoInInventory > 0)
            {
                if(_isEnemy == false)
                    Signals.Get<OnStartReloadWeapon>().Dispatch();
                ReloadWeapon();
            }
            else
                InvokeFireFromWeapon();
        }

        protected virtual bool IsBulletReloading()
        {
            return false;
        }

        private void ReloadWeapon()
        {
            if (CurrentCountAmmoInGun == _countAmmoInClip)
            {
                if(_isEnemy == false)
                    Signals.Get<OnFinishReloadWeapon>().Dispatch();
                IsMainReloading = false;
                InvokeFireFromWeapon();
                return;
            }
            var currentTime = 0;
            audioComponent.PlayAudioReloading();
            _tweenReloading = DOTween.To(() => currentTime, x => currentTime = x, 1, timeReload)
                .OnUpdate(() => CheckStateWeapon()).OnComplete(() => AddAmmoInGun());
        }

        private void CheckStateWeapon()
        {
            if (_isActive == false)
            {
                _tweenReloading.Kill(false);
                _tweenFire.Kill();
                this.gameObject.SetActive(false);
            }
        }

        private void AddAmmoInGun()
        {
            var isBullets = IsBulletReloading();
            var countAmmo = isBullets ? 1:_countAmmoInClip - CurrentCountAmmoInGun;
            var currentCountBulletsInInventory = InventoryPlayer.UseItem(_ammoInfo, countAmmo);
            if (isBullets)
            {
                if (currentCountBulletsInInventory > 0 & CurrentCountAmmoInGun < _ammoInfo.CountBullet)
                {
                    CurrentCountAmmoInGun += currentCountBulletsInInventory;
                    InvokeFireFromWeapon();
                    ReloadWeapon();
                    return;
                }
            }
            else
            {
                CurrentCountAmmoInGun += currentCountBulletsInInventory;
            }
            if(_isEnemy == false)
                Signals.Get<OnFinishReloadWeapon>().Dispatch();
            IsMainReloading = false;
            InvokeFireFromWeapon();
        }

        protected void WaitFire()
        {
            var currentTime = 0;
            _tweenFire = DOTween.To(() => currentTime, x => currentTime = x, 1, speedAttack)
                .OnStart(() => StartFire()).OnUpdate(() => CheckStateWeapon()).OnComplete(() => FinishFire());
        }

        private void StartFire()
        {
            audioComponent.PlayAudioFire();
            _isReloading = true;
            if(animator != null)animator.SetTrigger("Fire");
        }

        private void FinishFire()
        {
            _isReloading = false;
            if (isAutomatic & _isFire)
            {
                DoFire();
            }
        }

        private void OnDestroy()
        {
            
            if(_isEnemy == false)Signals.Get<OnUpgradeSpeedReloading>().RemoveListener(UpgradeSpeedReloading);
            if (OnDeleteBullets != null) OnDeleteBullets.Invoke();
        }
    }
}