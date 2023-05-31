using UnityEngine;

namespace _Source.FireSystem
{
    public abstract class ABulletController : MonoBehaviour
    {
        [SerializeField] protected LayerMask contactLayer;
        protected float SpeedMoving;
        protected float Damage;
        protected Rigidbody2D Rb;
        protected IPoolBullets PoolBullets;
        protected bool IsDelete = false;

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
            if(IsDelete)
                Destroy(this.gameObject);
            else
            {
                this.gameObject.SetActive(false);
                PoolBullets.ReturnBulletInPool(this);
            }
        }

        protected void DeleteBullet()
        {
            PoolBullets.OnDeleteBullets -= DeleteBullet;
            if (this.enabled ==false)
            {
                Destroy(this.gameObject);
                return;
            }
            IsDelete = true;
        }
    }
}