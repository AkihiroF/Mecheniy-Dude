using System.Collections.Generic;
using System.Linq;
using _Source.FireSystem.SOs;
using _Source.FireSystem.Weapons;
using _Source.InputSystem;
using _Source.Player;
using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using _Source.SignalsEvents.UIEvents;
using _Source.SignalsEvents.UpgradesEvents;
using _Source.SignalsEvents.WeaponsEvents;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public class PlayerFireSystem : MonoBehaviour
    {
        [SerializeField] private Transform pointPositionGun;
        [SerializeField] private PlayerGunSo firstGun;

        private PlayerGunSo _currentGunSo;
        private ABaseGunComponent _currentGun;
        private ClipSo _currentClip;
        private int _currentCountAmmo;
        private float _percentUpgrade;

        private Dictionary<PlayerGunSo,ABaseGunComponent> _currentGunInventory;

        private void Start()
        {
            _currentGunInventory = new Dictionary<PlayerGunSo, ABaseGunComponent>();
            Signals.Get<OnRestart>().AddListener(UnSubscribe);
            Signals.Get<OnFinishReloadWeapon>().AddListener(PrintAmmo);
            Signals.Get<OnUpgradeSpeedReloading>().AddListener(UpgradeReloading);
            
            if (_currentGunSo == null)
            {
                _currentGunSo = firstGun;
            }

            if (InventoryPlayer.GetCountWeapon != 0)
            {
                var keys = InventoryPlayer.GunSos.Keys;
                foreach (var key in keys)
                {
                    var gun = InventoryPlayer.GunSos[key];
                    if(gun == _currentGunSo)
                        continue;
                    var gunObj = Instantiate(gun.GunObjectObject, pointPositionGun);
                    var gunComponent = gunObj.GetComponent<ABaseGunComponent>();
                    _currentGunInventory.Add(gun, gunComponent);
                    gunComponent.SetParameters(gun.ClipInfo,0,0);
                    gunObj.SetActive(false);
                }
            }
            CreateWeapon();
        }

        private void UpgradeReloading(float percent)
        {
            _percentUpgrade += percent;
        }

        private void CreateWeapon()
        {
            _currentClip = _currentGunSo.ClipInfo;
            if (WeaponInInInventory(_currentGunSo))
            {
                var gun = _currentGunInventory[_currentGunSo];
                gun.gameObject.SetActive(true);
                _currentGun = gun;
            }
            else
            {
                var gunObj = Instantiate(_currentGunSo.GunObjectObject, pointPositionGun);
                _currentGun = gunObj.GetComponent<ABaseGunComponent>();
                _currentGunInventory.Add(_currentGunSo, _currentGun);
                Signals.Get<OnAddWeaponToPanel>().Dispatch(_currentGunSo.IconGun);
                SetParamInGun();
            }
            _currentGun.OnFireFromWeapon += UpdateCurrentCountAmmoInGun;
            if (InventoryPlayer.GetWeapon(_currentGun.GetType()) == null)
            {
                InventoryPlayer.AddWeapon(_currentGun.GetType(), _currentGunSo);
                InventoryPlayer.AddItem(_currentClip, _currentClip.CountBullet);
            }
            Signals.Get<OnUpdateIconWeapon>().Dispatch(_currentGunSo.IconGun);
            Signals.Get<OnSwitchFireMode>().Dispatch(_currentGun.isAutomatic);
            _currentGun.InvokeFireFromWeapon();
        }

        private void UnSubscribe()
        {
            _currentGun.OnFireFromWeapon -= UpdateCurrentCountAmmoInGun;
            Signals.Get<OnFinishReloadWeapon>().RemoveListener(PrintAmmo);
            Signals.Get<OnRestart>().RemoveListener(UnSubscribe);
        }

        private void SetParamInGun()
        {
            _currentGun.SetParameters(_currentClip,_currentCountAmmo,_percentUpgrade);
        }

        private void UpdateCurrentCountAmmoInGun(int count)
        {
            _currentCountAmmo = count;
            PrintAmmo();
        }


        public void PrintAmmo()
        {
            var currentPath = InventoryPlayer.GetCountItem(_currentClip);
            if (currentPath == -1) currentPath = 0;
            Signals.Get<OnPrintInfoAboutFire>().Dispatch(
                ($"{_currentCountAmmo} / {currentPath}"));
        }

        public void Fire()
        {
            _currentGun.Fire();
        }

        public void SwitchWeapon( WeaponsTypes id)
        {
            PlayerGunSo weapon;
            switch (id)
            {
                case WeaponsTypes.Knife:
                    weapon = InventoryPlayer.GetWeapon(typeof(KnifeComponent));
                    break;
                case WeaponsTypes.Pistol:
                    weapon = InventoryPlayer.GetWeapon(typeof(PistolComponent));
                    break;
                case WeaponsTypes.ShortGun:
                    weapon = InventoryPlayer.GetWeapon(typeof(ShortGunComponent));
                    break;
                case WeaponsTypes.Rifle:
                    weapon = InventoryPlayer.GetWeapon(typeof(RifleComponent));
                    break;
                default:
                    weapon = null;
                    Debug.Log("Idi nahui");
                    break;
            }
            if (weapon is not null)
            {
                if(_currentGunSo == weapon)
                    return;
                SwitchingOnNewWeapon(weapon);
            }
        }

        private void SwitchingOnNewWeapon(PlayerGunSo weapon)
        {
            Signals.Get<OnFinishReloadWeapon>().Dispatch();
            _currentGun.OnFireFromWeapon -= UpdateCurrentCountAmmoInGun;
            _currentGun.gameObject.SetActive(false);
            _currentGunSo = weapon;
            //InventoryPlayer.AddItem(_currentClip,_currentCountAmmo);
            _currentCountAmmo = 0;
            CreateWeapon();
        }

        private bool WeaponInInInventory(PlayerGunSo gun)
        {
            return _currentGunInventory.ContainsKey(gun);
        }

        public void ReloadWeapon()
        {
            _currentGun.StartReloadWeapon();
        }
        
    }
}
