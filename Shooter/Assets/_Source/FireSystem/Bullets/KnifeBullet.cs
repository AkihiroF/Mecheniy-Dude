using System;
using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class KnifeBullet : ABulletController
    {
        private bool _isFire;
        private Vector3 _startPosition;
        private Vector2 _controlPosition;
        private Transform _finishPosition;
        private float _duration;
        private float _count;
        public override bool FireBullet(float angle = 0)
        {
            if (_isFire)
                return false;
            this.gameObject.SetActive(true);
            _startPosition = transform.position;
            _controlPosition = _startPosition + (_finishPosition.position - _startPosition)/2 + transform.up * _duration;
            _isFire = true;
            _count = 0;
            return true;
        }

        public override void SetParameters(IPoolBullets controller, float speed, float damage,float duration, Transform endPosition)
        {
            base.SetParameters(controller, speed, damage,duration, endPosition);
            _duration = duration;
            _finishPosition = endPosition;
        }

        private void FixedUpdate()
        {
            if (_isFire)
            {
                if (_count < SpeedMoving) {
                    _count += SpeedMoving *Time.deltaTime;

                    Vector3 m1 = Vector3.Lerp( _startPosition, _controlPosition, _count );
                    Vector3 m2 = Vector3.Lerp( _controlPosition, _finishPosition.position, _count );
                    transform.position = Vector3.Lerp(m1, m2, _count);
                }

                if (transform.position == _finishPosition.position)
                {
                    _isFire = false;
                    PoolBullets.ReturnBulletInPool(this);
                    this.gameObject.SetActive(false);
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.gameObject;
            
            if ((contactLayer.value & (1 << obj.layer)) > 0)
                obj.GetComponent<ABaseHealth>().GetDamage(Damage);
            
        }
    }
}