using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.FireSystem.SOs
{
    [CreateAssetMenu(menuName = "Fire System/PlayerGun", fileName = "Gun")]
    public class PlayerGunSo : ScriptableObject
    {
        [SerializeField] private GameObject gunObject;
        [SerializeField] private ClipSo ammo;
        [SerializeField] private Sprite iconGun;

        public ClipSo ClipInfo
        {
            get
            {
                return ammo;
            }
        }
        public GameObject GunObjectObject
        {
            get
            {
                return gunObject;
            }
        }

        public Sprite IconGun
        {
            get
            {
                return iconGun;
            }
        }
    }
}