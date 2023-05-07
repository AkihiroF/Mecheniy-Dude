using _Source.FireSystem.Player;
using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem
{
    public abstract class ABulletController : MonoBehaviour
    {
        [SerializeField] protected LayerMask contactLayer;
        protected float SpeedMoving;
        protected float Damage;
        protected Rigidbody2D Rb;
        protected ABaseGunController Controller;

        public virtual void SetParameters(ABaseGunController controller, float speed, float damage)
        {
            Controller = controller;
            SpeedMoving = speed;
            Rb = GetComponent<Rigidbody2D>();
            Damage = damage;
        }

        public abstract void FireBullet();
        protected abstract void OnTriggerEnter2D(Collider2D col);
    }
}