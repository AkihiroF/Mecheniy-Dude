using System;
using System.Collections.Generic;
using _Source.FireSystem.Player;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class ShortGunBullet : ABulletController, IPoolBullets
    {
        [SerializeField] private ABulletController blastObject;
        [SerializeField] private int countBlasts;
        [SerializeField] private float degreeOfScattering;

        private List<ABulletController> poolBlasts;
        private float angleFire;

        public override void SetParameters(IPoolBullets controller, float speed, float damage)
        {
            base.SetParameters(controller, speed, damage);
            angleFire = degreeOfScattering / countBlasts;
            poolBlasts = new List<ABulletController>();
            for (int i = 0; i < countBlasts; i++)
            {
                var blast = Instantiate(blastObject,transform.parent).GetComponent<ABulletController>();
                blast.gameObject.SetActive(false);
                blast.SetParameters(this, speed,damage);
                poolBlasts.Add(blast);
            }
        }

        public override bool FireBullet(float angle = 0)
        {
            if (poolBlasts.Count == countBlasts)
            {
                FireOnWeapon();
                return true;
            }
            return false;
            }

        private void FireOnWeapon()
        {
            var currentAngle = -angleFire * countBlasts / 2;
            this.gameObject.SetActive(true);
            Rb.AddForce(transform.up * SpeedMoving);
            for (int i = 0; i < countBlasts; i++)
            {
                var blast = poolBlasts[i];
                var transform1 = blast.transform;
                var transform2 = transform;
                transform1.position = transform2.position;
                transform1.rotation = transform2.rotation;
                blast.FireBullet(currentAngle);
                currentAngle += angleFire;
            }
            poolBlasts.Clear();
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            this.gameObject.SetActive(false);
            PoolBullets.ReturnBulletInPool(this);
        }

        public void ReturnBulletInPool(ABulletController aBullet)
        {
            poolBlasts.Add(aBullet);
        }
    }
}