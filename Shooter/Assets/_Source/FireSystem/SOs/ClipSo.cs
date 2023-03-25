using UnityEngine;

namespace _Source.FireSystem.SOs
{
    [CreateAssetMenu(menuName = "Fire System/ClipAmmo", fileName = "Clip")]
    public class ClipSo : ScriptableObject
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private int countBulletInClip;
        [SerializeField] private float speedBullet;
        [SerializeField] private float damage;

        public GameObject BulletPrefab
        {
            get
            {
                return bullet;
            }
        }
        public int CountBullet
        {
            get
            {
                return  countBulletInClip;
            }
        }
        public float SpeedBullet
        {
            get
            {
                return speedBullet;
            }
        }        
        public float Damage
        {
            get
            {
                return damage;
            }
        }
    }
}