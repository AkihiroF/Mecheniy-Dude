using System.Collections.Generic;
using _Source.FireSystem.SOs;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        [SerializeField] private Transform pointExitBullet;
        private int _countAmmo;
        private int _countReload;
        private int _currentCountAmmo;
        private float _speedBullet;
        private float _damage;
        private GameObject _bulletObject;
        private List<BulletController> _bulletPool;
        

        public void ReturnBulletInPool(BulletController bullet)
        {
            _bulletPool.Add(bullet);
        }

        public void SetParameters(ClipSo info)
        {
            _countAmmo = info.CountBullet;
            _currentCountAmmo = _countAmmo;
            _bulletObject = info.BulletPrefab;
            _countReload = info.CountClips;
            _speedBullet = info.SpeedBullet;
            _damage = info.Damage;
            _bulletPool = new List<BulletController>();
        }

        private void InitialiseBullet()
        {
            if (_bulletPool.Count > 0)
            {
                var bullet = _bulletPool[0];
                SetPositionBullet(bullet.transform);
                bullet.FireBullet();
                _bulletPool.RemoveAt(0);
                _currentCountAmmo--;
            }
            else
            {
                var newBullet = Instantiate(_bulletObject)
                    .GetComponent<BulletController>();
                newBullet.SetParameters(this, _speedBullet, _damage);
                SetPositionBullet(newBullet.transform);
                newBullet.FireBullet();
                _currentCountAmmo--;
            }
        }

        private void SetPositionBullet(Transform bullet)
        {
            bullet.position = pointExitBullet.position;
            bullet.rotation = transform.rotation;
        }
        public void Fire()
        {
            if (_currentCountAmmo > 0)
            {
                InitialiseBullet();
            }
            else
            {
                if (_countReload > 0)
                {
                    Debug.Log("Reload");
                    _currentCountAmmo = _countAmmo;
                    _countReload--;
                    InitialiseBullet();
                }
            }
        }
    }
}