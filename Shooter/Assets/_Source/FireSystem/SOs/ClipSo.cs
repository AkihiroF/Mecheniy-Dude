using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.FireSystem.SOs
{
    [CreateAssetMenu(menuName = "Fire System/ClipAmmo", fileName = "Clip")]
    public class ClipSo : ScriptableObject
    {
        [SerializeField] private GameObject bulletObject;
        [SerializeField] private int countBulletInClip;
        [SerializeField] private float speedBullet;
        [SerializeField] private float damage;

        public GameObject BulletObjectPrefab
        {
            get
            {
                return bulletObject;
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