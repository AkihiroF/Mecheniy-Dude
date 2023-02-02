using _Source.FireSystem.Player.SOs;
using UnityEngine;

namespace _Source.FireSystem.Player
{
    public class PlayerFireSystem : MonoBehaviour
    {
        [SerializeField] private Transform pointPositionGun;
        [SerializeField] private PlayerGunSo firstGun;
        private GameObject _gunObj;
        private PlayerGunController _currentGun;

        private void Start()
        {
            if (_currentGun == null)
            {
                _gunObj = Instantiate(firstGun.GunObject, pointPositionGun);
                _currentGun = _gunObj.GetComponent<PlayerGunController>();
                SetParamInGun();
            }
        }

        private void SetParamInGun()
        {
            _currentGun.SetParameters(firstGun.CountAmmo, firstGun.BulletObject, firstGun.CountClip, firstGun.SpeedBullet);
        }

        public void Fire()
        {
            _currentGun.Fire();
        }
    }
}
