using UnityEngine;

namespace _Source.FireSystem
{
    public class AudioWeaponController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioFire;
        [SerializeField] private AudioSource audioReloading;

        public void PlayAudioFire()
        {
            if(audioFire!= null)
                audioFire.Play();
        }
        public void PlayAudioReloading()
        {
            if(audioReloading!= null)
                audioReloading.Play();
        }
    }
}