using System;
using System.Collections;
using System.Collections.Generic;
using _Source.FireSystem.SOs;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using _Source.SignalsEvents.WeaponsEvents;
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

        private bool _isMainReloading;
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
            _isMainReloading = false;
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

        public void InvokeFireFromWeapon()
        {
            if (OnFireFromWeapon != null)
                OnFireFromWeapon.Invoke(CurrentCountAmmoInGun);
        }

        public void StartReloadWeapon()
        {
            if(_isMainReloading)
                return;
            if(CurrentCountAmmoInGun == _countAmmoInClip)
                return;
            _isMainReloading = true;
            var currentCountAmmoInInventory = InventoryPlayer.UseItem(_ammoInfo, _countAmmoInClip - CurrentCountAmmoInGun);
            if (currentCountAmmoInInventory > 0)
            {
                if(_isEnemy == false)
                    Signals.Get<OnStartReloadWeapon>().Dispatch();
                StartCoroutine(ReloadWeapon(currentCountAmmoInInventory));
            }
            else
                InvokeFireFromWeapon();
        }

        private IEnumerator ReloadWeapon(int countAmmo)
        {
            audioComponent.PlayAudioReloading();
            yield return new WaitForSeconds(timeReload);
            CurrentCountAmmoInGun += countAmmo;
            if(_isEnemy == false)
                Signals.Get<OnFinishReloadWeapon>().Dispatch();
            _isMainReloading = false;
            InvokeFireFromWeapon();
        }

        protected IEnumerator WaitFire()
        {
            audioComponent.PlayAudioFire();
            _isReloading = true;
            if(animator != null)animator.SetTrigger("Fire");
            yield return new WaitForSeconds(speedAttack);
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