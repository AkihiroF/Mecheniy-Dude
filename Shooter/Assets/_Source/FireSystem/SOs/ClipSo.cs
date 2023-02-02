using UnityEngine;

namespace _Source.FireSystem.Player.SOs
{
    [CreateAssetMenu(menuName = "Fire System/ClipAmmo", fileName = "Clip")]
    public class ClipSo : ScriptableObject
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private int countBulletInClip;
        [SerializeField] private int countClips;
        [SerializeField] private float speedBullet;

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

        public int CountClips
        {
            get
            {
                return countClips;
            }
        }

        public float SpeedBullet
        {
            get
            {
                return speedBullet;
            }
        }
    }
}