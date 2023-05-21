using UnityEngine;

namespace _Source.FireSystem.Weapons
{
    public class KnifeController : ABaseGunController
    {
        [SerializeField] private Transform endPosition;
        [SerializeField] private float duration;
        protected override void InitialiseBullet()
        {
            if (BulletPool.Count == 1)
            {
                SetPositionBullet(BulletPool[0].transform);
                BulletPool[0].FireBullet();
                BulletPool.RemoveAt(0);
            }
            else
            {
                var newBullet = Instantiate(BulletObject, this.transform)
                    .GetComponent<ABulletController>();
                SetPositionBullet(newBullet.transform);
                newBullet.SetParameters(this, SpeedBullet, Damage, duration, endPosition);
                newBullet.FireBullet();
            }
        }

        protected override void DoFire()
        {
            InitialiseBullet();
            StartCoroutine(WaitFire());
        }
    }
}