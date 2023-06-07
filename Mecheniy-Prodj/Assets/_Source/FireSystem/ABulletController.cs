using UnityEngine;

namespace _Source.FireSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ABulletController : MonoBehaviour
    {
        [SerializeField] protected LayerMask contactLayer;
        protected float SpeedMoving;
        protected float Damage;
        protected Rigidbody2D Rb;
        protected IPoolBullets PoolBullets;
        private bool _isDelete = false;

        public virtual void SetParameters(IPoolBullets controller, float speed, float damage)
        {
            PoolBullets = controller;
            SpeedMoving = speed;
            Rb = GetComponent<Rigidbody2D>();
            Damage = damage;
            PoolBullets.OnDeleteBullets += DeleteBullet;
        }

        public virtual void SetParameters(IPoolBullets controller, float speed, float damage, float duration,  Transform endPosition)
        {
            SetParameters(controller,speed,damage);
        }

        public abstract bool FireBullet(float angle = 0);

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if(_isDelete)
                Destroy(this.gameObject);
            else
            {
                this.gameObject.SetActive(false);
                PoolBullets.ReturnBulletInPool(this);
            }
        }

        private void DeleteBullet()
        {
            PoolBullets.OnDeleteBullets -= DeleteBullet;
            if (this.enabled ==false)
            {
                Destroy(this.gameObject);
                return;
            }
            _isDelete = true;
        }
    }
}