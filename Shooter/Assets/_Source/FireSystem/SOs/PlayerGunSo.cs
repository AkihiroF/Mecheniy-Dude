using UnityEngine;

namespace _Source.FireSystem.Player.SOs
{
    [CreateAssetMenu(menuName = "Fire System/PlayerGun", fileName = "Gun")]
    public class PlayerGunSo : ScriptableObject
    {
        [SerializeField] private GameObject gun;
        [SerializeField] private ClipSo ammo;

        public int CountAmmo
        {
            get
            {
                return ammo.CountBullet;
            }
        }

        public int CountClip
        {
            get
            {
                return ammo.CountClips;
            }
        }

        public float SpeedBullet
        {
            get
            {
                return ammo.SpeedBullet;
            }
        }

        public GameObject GunObject
        {
            get
            {
                return gun;
            }
        }

        public GameObject BulletObject
        {
            get
            {
                return ammo.BulletPrefab;
            }
        }
    }
}