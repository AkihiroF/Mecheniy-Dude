using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class ShortGunBullet : ABulletController, IPoolBullets
    {
        [SerializeField] private ABulletController blastObject;
        [SerializeField] private int countBlasts;
        [SerializeField] private float degreeOfScattering;

        private List<ABulletController> _poolBlasts;
        private float _angleFire;

        public override void SetParameters(IPoolBullets controller, float speed, float damage)
        {
            base.SetParameters(controller, speed, damage);
            _angleFire = degreeOfScattering / countBlasts;
            _poolBlasts = new List<ABulletController>();
            for (int i = 0; i < countBlasts; i++)
            {
                var blast = Instantiate(blastObject,transform.parent).GetComponent<ABulletController>();
                blast.gameObject.SetActive(false);
                blast.SetParameters(this, speed,damage);
                _poolBlasts.Add(blast);
            }
        }

        public override bool FireBullet(float angle = 0)
        {
            if (_poolBlasts.Count == countBlasts)
            {
                FireOnWeapon();
                return true;
            }
            return false;
            }

        private void FireOnWeapon()
        {
            var currentAngle = -_angleFire * countBlasts / 2;
            this.gameObject.SetActive(true);
            Rb.AddForce(transform.up * SpeedMoving);
            for (int i = 0; i < countBlasts; i++)
            {
                var blast = _poolBlasts[i];
                var transform1 = blast.transform;
                var transform2 = transform;
                transform1.position = transform2.position;
                transform1.rotation = transform2.rotation;
                blast.FireBullet(currentAngle);
                currentAngle += _angleFire;
            }
            _poolBlasts.Clear();
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            this.gameObject.SetActive(false);
            PoolBullets.ReturnBulletInPool(this);
        }

        public event Action OnDeleteBullets;

        private void OnDestroy()
        {
            if (OnDeleteBullets != null) OnDeleteBullets.Invoke();
        }

        public void ReturnBulletInPool(ABulletController aBullet)
        {
            _poolBlasts.Add(aBullet);
        }
    }
}