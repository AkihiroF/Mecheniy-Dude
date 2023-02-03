using UnityEngine;

namespace _Source.FireSystem.SOs
{
    [CreateAssetMenu(menuName = "Fire System/PlayerGun", fileName = "Gun")]
    public class PlayerGunSo : ScriptableObject
    {
        [SerializeField] private GameObject gun;
        [SerializeField] private ClipSo ammo;

        public ClipSo ClipInfo
        {
            get
            {
                return ammo;
            }
        }
        public GameObject GunObject
        {
            get
            {
                return gun;
            }
        }
    }
}